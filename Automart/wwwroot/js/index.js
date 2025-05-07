const carListContainer = document.getElementById("car-list");
const searchInput = document.getElementById("search-input");

let cars = [];

// Fetch car data from backend
fetch("/CarListing/GetAllCars")
    .then(response => response.json())
    .then(data => {
        cars = data;
        carListContainer.innerHTML = ""; // Start empty
    })
    .catch(error => {
        console.error("Failed to load cars:", error);
        carListContainer.innerHTML = "<p>Error loading car data.</p>";
    });

// Display matching cars
function displayCars(list) {
    carListContainer.innerHTML = "";

    if (list.length === 0) {
        carListContainer.innerHTML = "<p>No matching cars found.</p>";
        return;
    }

    list.forEach(car => {
        const card = document.createElement("div");
        card.className = "car-card";

        card.innerHTML = `
      <img src="${car.photos[0]}" alt="${car.brand} ${car.model}">
      <div class="car-info">
        <h3>${car.brand} ${car.model} (${car.year})</h3>
        <p><strong>Type:</strong> ${car.carType}</p>
        <p><strong>Fuel:</strong> ${car.fuelType}</p>
        <p><strong>Mileage:</strong> ${car.mileage ? car.mileage.toLocaleString() + " km" : "N/A"}</p>
        <p><strong>Price:</strong> $${car.price.toLocaleString()}</p>
      </div>
    `;

        carListContainer.appendChild(card);
    });
}

// Handle live search
searchInput.addEventListener("input", () => {
    const keyword = searchInput.value.toLowerCase().trim();

    if (keyword === "") {
        carListContainer.innerHTML = ""; // Clear if empty
        return;
    }

    const filtered = cars.filter(car =>
        (car.brand && car.brand.toLowerCase().includes(keyword)) ||
        (car.model && car.model.toLowerCase().includes(keyword)) ||
        (car.year && car.year.toString().includes(keyword)) ||
        (car.carType && car.carType.toLowerCase().includes(keyword)) ||
        (car.fuelType && car.fuelType.toLowerCase().includes(keyword))
    );

    displayCars(filtered);
});
