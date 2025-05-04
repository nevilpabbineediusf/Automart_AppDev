document.getElementById("carForm").addEventListener("submit", async function (e) {
    e.preventDefault();
  
    const imageFile = document.getElementById("imageInput").files[0];
    if (!imageFile) return alert("Please upload an image.");
  
    // Convert image to Base64
    const toBase64 = (file) => new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result.split(',')[1]); // Remove data:image/...;base64,
      reader.onerror = error => reject(error);
    });
  
    try {
      const base64Image = await toBase64(imageFile);
  
      const formData = new FormData();
      formData.append("key", "46996ce3e2660fcba276709a1a84c3c2"); // your API key
      formData.append("image", base64Image);
  
      const response = await fetch("https://api.imgbb.com/1/upload", {
        method: "POST",
        body: formData,
      });
  
      const data = await response.json();
      if (!data.success) throw new Error("Image upload failed.");
  
      const imageUrl = data.data.display_url;
  
      // Show image
      document.getElementById("imagePreview").innerHTML = `<img src="${imageUrl}" alt="Uploaded Image">`;
  
      // Collect and show car details
      const carDetails = {
        brand: e.target.brand.value,
        model: e.target.model.value,
        year: e.target.year.value,
        price: e.target.price.value,
        imageUrl: imageUrl,
      };
  
      console.log("Car posted successfully:", carDetails);
      alert("Car posted successfully!");
  
    } catch (err) {
      alert("Error uploading image: " + err.message);
    }
  });
  