// ═══════════════════════════════════════
// MAIN PAGE — JAVASCRIPT
// ═══════════════════════════════════════

document.addEventListener('DOMContentLoaded', () => {
    initNavbar();
    initLanguageSwitch();
    initSmoothScroll();
    new Carousel();
    initVideoPlayer('heroVideo', 'videoOverlay', 'playBtn', 'videoControls', 'playPauseBtn', 'muteBtn', 'fullscreenBtn', 'progressBar', 'progressFilled');
    initVideoPlayer('featuredVideo', 'featuredVideoOverlay', 'featuredPlayBtn', 'featuredVideoControls', 'featuredPlayPauseBtn', 'featuredMuteBtn', 'featuredFullscreenBtn', 'featuredProgressBar', 'featuredProgressFilled');
    initBackgroundVideo();
    initScrollAnimations();
    initNewsletter();
    initAuthPopup();
});

// ── Smooth Scrolling ────────────────────
function initSmoothScroll() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        });
    });
}

// ── Navbar ──────────────────────────────
function initNavbar() {
    const hamburger = document.querySelector('.hamburger');
    const navMenu = document.querySelector('.nav-menu');
    const navLinks = document.querySelectorAll('.nav-link');
    const navbar = document.querySelector('.navbar');

    if (hamburger) {
        hamburger.addEventListener('click', () => {
            hamburger.classList.toggle('active');
            navMenu.classList.toggle('active');
        });
    }

    navLinks.forEach(link => {
        link.addEventListener('click', () => {
            if (hamburger) hamburger.classList.remove('active');
            if (navMenu) navMenu.classList.remove('active');
        });
    });

    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
        updateActiveLink();
    });
}

function updateActiveLink() {
    const sections = document.querySelectorAll('section[id]');
    const navLinks = document.querySelectorAll('.nav-link');
    let current = 'home';

    sections.forEach(section => {
        const sectionTop = section.offsetTop;
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
}

// ── Language Switch ─────────────────────
function initLanguageSwitch() {
    const langButtons = document.querySelectorAll('.lang-switch-btn');
    const switchSlider = document.querySelector('.switch-slider');

    langButtons.forEach((btn, index) => {
        btn.addEventListener('click', () => {
            langButtons.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
            if (switchSlider) {
                switchSlider.style.transform = index === 0 ? 'translateX(0)' : 'translateX(100%)';
            }
        });
    });
}

// ── Scroll Animations ───────────────────
function initScrollAnimations() {
    const targets = document.querySelectorAll('.solutions-table, .media-content');
    targets.forEach(el => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(20px)';
        el.style.transition = 'all 0.6s ease-out';
    });

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.1, rootMargin: '0px 0px -50px 0px' });

    targets.forEach(el => observer.observe(el));
}

// ── Carousel ────────────────────────────
class Carousel {
    constructor() {
        this.currentSlide = 0;
        this.slides = document.querySelectorAll('.carousel-slide');
        this.dots = document.querySelectorAll('.dot');
        this.prevBtn = document.querySelector('.carousel-btn-prev');
        this.nextBtn = document.querySelector('.carousel-btn-next');
        this.track = document.querySelector('.carousel-track');

        if (!this.track || !this.slides.length) return;
        this.init();
    }

    init() {
        this.updateCarousel();
        this.nextBtn.addEventListener('click', () => this.nextSlide());
        this.prevBtn.addEventListener('click', () => this.prevSlide());

        this.dots.forEach((dot, index) => {
            dot.addEventListener('click', () => this.goToSlide(index));
        });

        document.addEventListener('keydown', (e) => {
            if (e.key === 'ArrowLeft') this.prevSlide();
            if (e.key === 'ArrowRight') this.nextSlide();
        });

        let touchStartX = 0;
        this.track.addEventListener('touchstart', (e) => {
            touchStartX = e.changedTouches[0].screenX;
        });
        this.track.addEventListener('touchend', (e) => {
            const touchEndX = e.changedTouches[0].screenX;
            if (touchEndX < touchStartX - 50) this.nextSlide();
            if (touchEndX > touchStartX + 50) this.prevSlide();
        });
    }

    updateCarousel() {
        this.track.style.transform = `translateX(-${this.currentSlide * 100}%)`;
        this.slides.forEach((slide, i) => slide.classList.toggle('active', i === this.currentSlide));
        this.dots.forEach((dot, i) => dot.classList.toggle('active', i === this.currentSlide));
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

// ── Background Video ────────────────────
function initBackgroundVideo() {
    const bgVideo = document.querySelector('.hero-background-video');
    if (!bgVideo) return;

    bgVideo.addEventListener('error', (e) => {
        console.error('Background video error:', e);
    });

    setTimeout(() => {
        bgVideo.play().catch(error => {
            console.error('Background video autoplay failed:', error);
        });
    }, 100);
}

// ── Video Player ────────────────────────
function initVideoPlayer(videoId, overlayId, playBtnId, controlsId, playPauseBtnId, muteBtnId, fullscreenBtnId, progressBarId, progressFilledId) {
    const video = document.getElementById(videoId);
    const overlay = document.getElementById(overlayId);
    const controls = document.getElementById(controlsId);
    const playPauseBtn = document.getElementById(playPauseBtnId);
    const muteBtn = document.getElementById(muteBtnId);
    const fullscreenBtn = document.getElementById(fullscreenBtnId);
    const progressBar = document.getElementById(progressBarId);
    const progressFilled = document.getElementById(progressFilledId);

    if (!video || !overlay) return;

    overlay.addEventListener('click', () => {
        video.play().then(() => {
            overlay.classList.add('hidden');
        }).catch(error => console.error('Play error:', error));
    });

    if (playPauseBtn) {
        playPauseBtn.addEventListener('click', () => {
            if (video.paused) {
                video.play().catch(e => console.error('Play error:', e));
            } else {
                video.pause();
            }
        });
    }

    video.addEventListener('play', () => {
        if (playPauseBtn) {
            playPauseBtn.querySelector('.play-icon').style.display = 'none';
            playPauseBtn.querySelector('.pause-icon').style.display = 'block';
        }
    });

    video.addEventListener('pause', () => {
        if (playPauseBtn) {
            playPauseBtn.querySelector('.play-icon').style.display = 'block';
            playPauseBtn.querySelector('.pause-icon').style.display = 'none';
        }
    });

    if (muteBtn) {
        muteBtn.addEventListener('click', () => {
            video.muted = !video.muted;
            muteBtn.querySelector('.unmute-icon').style.display = video.muted ? 'none' : 'block';
            muteBtn.querySelector('.mute-icon').style.display = video.muted ? 'block' : 'none';
        });
    }

    if (fullscreenBtn) {
        fullscreenBtn.addEventListener('click', () => {
            const wrapper = video.closest('.hero-video-wrapper');
            if (wrapper) {
                if (wrapper.requestFullscreen) wrapper.requestFullscreen();
                else if (wrapper.webkitRequestFullscreen) wrapper.webkitRequestFullscreen();
                else if (wrapper.msRequestFullscreen) wrapper.msRequestFullscreen();
            }
        });
    }

    if (progressBar && progressFilled) {
        video.addEventListener('timeupdate', () => {
            if (video.duration) {
                progressFilled.style.width = (video.currentTime / video.duration) * 100 + '%';
            }
        });

        progressBar.addEventListener('click', (e) => {
            const rect = progressBar.getBoundingClientRect();
            video.currentTime = ((e.clientX - rect.left) / rect.width) * video.duration;
        });
    }

    video.addEventListener('ended', () => {
        overlay.classList.remove('hidden');
        if (controls) controls.classList.remove('show');
    });

    const wrapper = video.closest('.hero-video-wrapper');
    if (wrapper && controls) {
        let controlsTimeout;
        wrapper.addEventListener('mousemove', () => {
            controls.classList.add('show');
            clearTimeout(controlsTimeout);
            controlsTimeout = setTimeout(() => {
                if (!video.paused) controls.classList.remove('show');
            }, 3000);
        });
    }
}

// ── Newsletter Popup ────────────────────
function initNewsletter() {
    const popup = document.getElementById('newsletterPopup');
    const closeBtn = document.getElementById('closePopup');
    const popupOverlay = popup ? popup.querySelector('.popup-overlay') : null;
    const newsletterForm = document.getElementById('newsletterForm');

    if (!popup || !closeBtn) return;

    setTimeout(() => {
        if (!sessionStorage.getItem('newsletterPopupShown')) {
            popup.classList.add('active');
            document.body.style.overflow = 'hidden';
            sessionStorage.setItem('newsletterPopupShown', 'true');
        }
    }, 5000);

    const closePopup = () => {
        popup.classList.remove('active');
        document.body.style.overflow = '';
    };

    closeBtn.addEventListener('click', closePopup);
    if (popupOverlay) popupOverlay.addEventListener('click', closePopup);

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && popup.classList.contains('active')) closePopup();
    });

    if (newsletterForm) {
        newsletterForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const formData = {
                firstName: newsletterForm.firstName.value,
                lastName: newsletterForm.lastName.value,
                email: newsletterForm.email.value,
                country: newsletterForm.country.value
            };
            console.log('Newsletter signup:', formData);
            alert('Thank you for signing up! We\'ll keep you updated with the latest news from Agarta AI.');
            closePopup();
            newsletterForm.reset();
        });
    }
}

// ── Auth Popup (Signup / Login) ─────────
function initAuthPopup() {
    const popup = document.getElementById('authPopup');
    const overlay = document.getElementById('authOverlay');
    const closeBtn = document.getElementById('authCloseBtn');
    const openBtn = document.getElementById('authOpenBtn');
    const loginView = document.getElementById('authLoginView');
    const signupView = document.getElementById('authSignupView');
    const goToSignup = document.getElementById('goToSignup');
    const goToLogin = document.getElementById('goToLogin');
    const loginForm = document.getElementById('loginForm');
    const signupForm = document.getElementById('signupForm');

    if (!popup || !openBtn) return;

    // Open popup (default to signup view)
    openBtn.addEventListener('click', () => {
        popup.classList.add('active');
        document.body.style.overflow = 'hidden';
        showView('signup');
    });

    // Close popup
    const closePopup = () => {
        popup.classList.remove('active');
        document.body.style.overflow = '';
    };

    closeBtn.addEventListener('click', closePopup);
    overlay.addEventListener('click', closePopup);

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && popup.classList.contains('active')) closePopup();
    });

    // Switch between views
    function showView(view) {
        loginView.classList.remove('active');
        signupView.classList.remove('active');
        if (view === 'login') {
            loginView.classList.add('active');
        } else {
            signupView.classList.add('active');
        }
    }

    goToSignup.addEventListener('click', (e) => {
        e.preventDefault();
        showView('signup');
    });

    goToLogin.addEventListener('click', (e) => {
        e.preventDefault();
        showView('login');
    });

    // Form submissions
    if (loginForm) {
        loginForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const email = loginForm.querySelector('input[type="email"]').value;
            console.log('Login attempt:', email);
            alert('Login functionality coming soon! Email: ' + email);
            closePopup();
            loginForm.reset();
        });
    }

    if (signupForm) {
        signupForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const email = signupForm.querySelector('input[type="email"]').value;
            console.log('Signup attempt:', email);
            alert('Signup functionality coming soon! Email: ' + email);
            closePopup();
            signupForm.reset();
        });
    }
}
