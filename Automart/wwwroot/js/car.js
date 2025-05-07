const carListContainer = document.getElementById("car-list");

// Fetch and display all available cars from backend
fetch("/CarListing/GetAllCars")
    .then(response => response.json())
    .then(cars => {
        displayCars(cars);
    })
    .catch(error => {
        console.error("Error fetching car data:", error);
        carListContainer.innerHTML = "<p>Failed to load car data.</p>";
    });

// Render all car cards
function displayCars(carArray) {
    carListContainer.innerHTML = "";

    if (carArray.length === 0) {
        carListContainer.innerHTML = "<p>No available cars at the moment.</p>";
        return;
    }

    carArray.forEach(car => {
        const mileageInfo = car.mileage !== null && car.mileage !== undefined
            ? `<p><strong>Mileage:</strong> ${Number(car.mileage).toLocaleString()} km</p>`
            : '';

        const carCard = document.createElement("div");
        carCard.className = "car-card";

        carCard.innerHTML = `
      <img src="${car.photos?.[0] || '/images/placeholder.jpg'}" alt="${car.brand} ${car.model}">
      <div class="car-info">
        <h3>${car.brand} ${car.model} (${car.year})</h3>
        <p><strong>Fuel:</strong> ${car.fuelType}</p>
        <p><strong>Type:</strong> ${car.carType}</p>
        ${mileageInfo}
        <p><strong>Price:</strong> $${Number(car.price).toLocaleString()}</p>
        <p><strong>Category:</strong> ${car.carTypeLabel}</p>
        <button class="location-btn" onclick="viewLocation('${(car.address || '').replace(/'/g, "\\'")}')">View Location</button>
      </div>
    `;

        carListContainer.appendChild(carCard);
    });
}

// 🌍 View address on map using Leaflet and OpenStreetMap
function viewLocation(address) {
    const mapModal = document.getElementById('mapModal');
    mapModal.style.display = 'block';

    // Geocode address to get coordinates
    fetch(`https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(address)}`)
        .then(response => response.json())
        .then(data => {
            if (data.length === 0) {
                alert("Location not found");
                return;
            }

            const { lat, lon } = data[0];

            // Clear existing map instance if any
            if (window.mapInstance) {
                window.mapInstance.remove();
            }

            const map = L.map('map').setView([lat, lon], 13);
            window.mapInstance = map;

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors'
            }).addTo(map);

            L.marker([lat, lon]).addTo(map).bindPopup(address).openPopup();
        });
}
