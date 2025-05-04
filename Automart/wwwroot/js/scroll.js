// scroll.js
function scrollFeatured(direction) {
    const container = document.getElementById('featuredScroll');
    const scrollAmount = 320;
    container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
  }
  