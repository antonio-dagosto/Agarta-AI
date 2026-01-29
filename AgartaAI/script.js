// Navbar functionality
function loadToursFromBackend() {
    fetch("http://localhost:8000/api/tours")
        .then(response => response.json())
        .then(data => {
            const container = document.getElementById("tours-container");
            container.innerHTML = "";

            if (data.length === 0) {
                container.innerHTML = "<p>No tours available.</p>";
                return;
            }

            data.forEach(tour => {
                const div = document.createElement("div");

                div.style.border = "1px solid #ccc";
                div.style.padding = "12px";
                div.style.margin = "10px 0";
                div.style.borderRadius = "6px";

                div.innerHTML = `
                    <h3>${tour.name}</h3>
                    <p>${tour.description}</p>
                    <button onclick="deleteTour(${tour.id})"
                        style="background:#c0392b;color:white;
                               border:none;padding:8px 12px;
                               border-radius:4px;cursor:pointer;">
                        Delete Tour
                    </button>
                `;

                container.appendChild(div);
            });
        })
        .catch(error => {
            document.getElementById("tours-container").innerText =
                "Failed to load tours from backend.";
            console.error(error);
        });
}

function deleteTour(tourId) {
    if (!confirm("Are you sure you want to delete this tour?")) {
        return;
    }

    fetch(`http://localhost:8000/api/tours/${tourId}`, {
        method: "DELETE"
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Failed to delete tour");
        }
        return response.json();
    })
    .then(() => {
        loadToursFromBackend(); // refresh list
    })
    .catch(error => {
        alert("Error deleting tour");
        console.error(error);
    });
}


document.addEventListener('DOMContentLoaded', () => {
    loadToursFromBackend();
    const hamburger = document.querySelector('.hamburger');
    const navMenu = document.querySelector('.nav-menu');
    const navLinks = document.querySelectorAll('.nav-link');
    const navbar = document.querySelector('.navbar');
    
    // Toggle mobile menu
    hamburger.addEventListener('click', () => {
        hamburger.classList.toggle('active');
        navMenu.classList.toggle('active');
    });
    
    // Close mobile menu when clicking on a link
    navLinks.forEach(link => {
        link.addEventListener('click', () => {
            hamburger.classList.remove('active');
            navMenu.classList.remove('active');
        });
    });
    
    // Navbar scroll effect
    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
    });
    
    // Active link on scroll
    const sections = document.querySelectorAll('section[id]');
    
    window.addEventListener('scroll', () => {
        let current = '';
        sections.forEach(section => {
            const sectionTop = section.offsetTop;
            const sectionHeight = section.clientHeight;
            if (window.scrollY >= (sectionTop - 100)) {
                current = section.getAttribute('id');
            }
        });
        
        navLinks.forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('href') === `#${current}`) {
                link.classList.add('active');
            }
        });
    });
    
    // Initialize other components
    new Carousel();
    initVideoPlayer();
    initBackgroundVideo();
});

// Smooth scrolling for internal links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth'
            });
        }
    });
});

// Button click handlers (you can customize these)
document.querySelectorAll('.btn, .explore-btn, .secondary-btn, .read-more-btn').forEach(button => {
    button.addEventListener('click', function() {
        console.log('Button clicked:', this.textContent);
        // Add your custom functionality here
    });
});

// Animate cards on scroll (excluding feature cards which have flip animation)
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver(function(entries) {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.style.opacity = '1';
            entry.target.style.transform = 'translateY(0)';
        }
    });
}, observerOptions);

// Only animate other cards, not feature cards
document.querySelectorAll('.solutions-table, .media-content').forEach(element => {
    element.style.opacity = '0';
    element.style.transform = 'translateY(20px)';
    element.style.transition = 'all 0.6s ease-out';
    observer.observe(element);
});

// Carousel functionality
class Carousel {
    constructor() {
        this.currentSlide = 0;
        this.slides = document.querySelectorAll('.carousel-slide');
        this.dots = document.querySelectorAll('.dot');
        this.prevBtn = document.querySelector('.carousel-btn-prev');
        this.nextBtn = document.querySelector('.carousel-btn-next');
        this.track = document.querySelector('.carousel-track');
        
        this.init();
    }
    
    init() {
        // Set initial state
        this.updateCarousel();
        
        // Event listeners
        this.nextBtn.addEventListener('click', () => this.nextSlide());
        this.prevBtn.addEventListener('click', () => this.prevSlide());
        
        this.dots.forEach((dot, index) => {
            dot.addEventListener('click', () => this.goToSlide(index));
        });
        
        // Keyboard navigation
        document.addEventListener('keydown', (e) => {
            if (e.key === 'ArrowLeft') this.prevSlide();
            if (e.key === 'ArrowRight') this.nextSlide();
        });
        
        // Touch/swipe support
        let touchStartX = 0;
        let touchEndX = 0;
        
        this.track.addEventListener('touchstart', (e) => {
            touchStartX = e.changedTouches[0].screenX;
        });
        
        this.track.addEventListener('touchend', (e) => {
            touchEndX = e.changedTouches[0].screenX;
            this.handleSwipe();
        });
        
        const handleSwipe = () => {
            if (touchEndX < touchStartX - 50) this.nextSlide();
            if (touchEndX > touchStartX + 50) this.prevSlide();
        };
        
        this.handleSwipe = handleSwipe;
    }
    
    updateCarousel() {
        // Update track position
        this.track.style.transform = `translateX(-${this.currentSlide * 100}%)`;
        
        // Update active slide
        this.slides.forEach((slide, index) => {
            slide.classList.toggle('active', index === this.currentSlide);
        });
        
        // Update active dot
        this.dots.forEach((dot, index) => {
            dot.classList.toggle('active', index === this.currentSlide);
        });
    }
    
    nextSlide() {
        this.currentSlide = (this.currentSlide + 1) % this.slides.length;
        this.updateCarousel();
    }
    
    prevSlide() {
        this.currentSlide = (this.currentSlide - 1 + this.slides.length) % this.slides.length;
        this.updateCarousel();
    }
    
    goToSlide(index) {
        this.currentSlide = index;
        this.updateCarousel();
    }
}

// Initialize carousel when DOM is loaded
// Carousel is already initialized in the main DOMContentLoaded above

// Background video debugging
function initBackgroundVideo() {
    const bgVideo = document.querySelector('.hero-background-video');
    if (bgVideo) {
        bgVideo.addEventListener('loadeddata', () => {
            console.log('Background video loaded successfully');
        });
        
        bgVideo.addEventListener('error', (e) => {
            console.error('Background video error:', e);
            console.error('Video src:', bgVideo.querySelector('source').src);
        });
        
        // Force play after a short delay
        setTimeout(() => {
            bgVideo.play().catch(error => {
                console.error('Background video autoplay failed:', error);
            });
        }, 100);
    }
}

// Video Player functionality
function initVideoPlayer() {
    const video = document.getElementById('heroVideo');
    const overlay = document.getElementById('videoOverlay');
    const playBtn = document.getElementById('playBtn');
    const controls = document.getElementById('videoControls');
    const playPauseBtn = document.getElementById('playPauseBtn');
    const muteBtn = document.getElementById('muteBtn');
    const fullscreenBtn = document.getElementById('fullscreenBtn');
    const progressBar = document.getElementById('progressBar');
    const progressFilled = document.getElementById('progressFilled');
    
    if (!video) {
        console.error('Video element not found');
        return;
    }
    
    // Log video loading status
    video.addEventListener('loadedmetadata', () => {
        console.log('Video metadata loaded');
    });
    
    video.addEventListener('error', (e) => {
        console.error('Video error:', e);
        console.error('Video error code:', video.error?.code);
        console.error('Video error message:', video.error?.message);
    });
    
    // Play video when overlay is clicked
    overlay.addEventListener('click', () => {
        const playPromise = video.play();
        if (playPromise !== undefined) {
            playPromise.then(() => {
                overlay.classList.add('hidden');
            }).catch(error => {
                console.error('Play error:', error);
            });
        }
    });
    
    // Play/Pause button
    playPauseBtn.addEventListener('click', () => {
        if (video.paused) {
            video.play().catch(error => console.error('Play error:', error));
        } else {
            video.pause();
        }
    });
    
    // Update play/pause icon
    video.addEventListener('play', () => {
        playPauseBtn.querySelector('.play-icon').style.display = 'none';
        playPauseBtn.querySelector('.pause-icon').style.display = 'block';
    });
    
    video.addEventListener('pause', () => {
        playPauseBtn.querySelector('.play-icon').style.display = 'block';
        playPauseBtn.querySelector('.pause-icon').style.display = 'none';
    });
    
    // Mute/Unmute button
    muteBtn.addEventListener('click', () => {
        video.muted = !video.muted;
        muteBtn.querySelector('.unmute-icon').style.display = video.muted ? 'none' : 'block';
        muteBtn.querySelector('.mute-icon').style.display = video.muted ? 'block' : 'none';
    });
    
    // Fullscreen button
    fullscreenBtn.addEventListener('click', () => {
        const wrapper = document.querySelector('.hero-video-wrapper');
        if (wrapper.requestFullscreen) {
            wrapper.requestFullscreen();
        } else if (wrapper.webkitRequestFullscreen) {
            wrapper.webkitRequestFullscreen();
        } else if (wrapper.msRequestFullscreen) {
            wrapper.msRequestFullscreen();
        }
    });
    
    // Progress bar
    video.addEventListener('timeupdate', () => {
        if (video.duration) {
            const percent = (video.currentTime / video.duration) * 100;
            progressFilled.style.width = percent + '%';
        }
    });
    
    // Click on progress bar to seek
    progressBar.addEventListener('click', (e) => {
        const rect = progressBar.getBoundingClientRect();
        const percent = (e.clientX - rect.left) / rect.width;
        video.currentTime = percent * video.duration;
    });
    
    // Show controls when video ends
    video.addEventListener('ended', () => {
        overlay.classList.remove('hidden');
        controls.classList.remove('show');
    });
    
    // Show controls on mouse move
    let controlsTimeout;
    document.querySelector('.hero-video-wrapper').addEventListener('mousemove', () => {
        controls.classList.add('show');
        clearTimeout(controlsTimeout);
        controlsTimeout = setTimeout(() => {
            if (!video.paused) {
                controls.classList.remove('show');
            }
        }, 3000);
    });
}