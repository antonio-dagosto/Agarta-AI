// ═══════════════════════════════════════
// EXPLORE PAGE — JAVASCRIPT
// ═══════════════════════════════════════

document.addEventListener('DOMContentLoaded', () => {
    initNavbar();
    initStars();
    initMapInteraction();
    initMapFilters();
    initTourTabs();
    initScrollAnimations();
    initLanguageSwitch();
    initSmoothScroll();
});

// ── Smooth Scrolling ────────────────────
function initSmoothScroll() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
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

// ── Language switch ──────────────────────
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

// ── Starfield ────────────────────────────
function initStars() {
    const container = document.getElementById('starsBg');
    if (!container) return;

    for (let i = 0; i < 180; i++) {
        const star = document.createElement('div');
        const size = Math.random() * 2.5 + 0.5;
        star.style.cssText = `
            position: absolute;
            width: ${size}px;
            height: ${size}px;
            background: white;
            border-radius: 50%;
            top: ${Math.random() * 100}%;
            left: ${Math.random() * 100}%;
            opacity: ${Math.random() * 0.7 + 0.1};
            animation: starTwinkle ${Math.random() * 4 + 2}s ease-in-out ${Math.random() * 4}s infinite;
        `;
        container.appendChild(star);
    }

    if (!document.getElementById('starStyle')) {
        const style = document.createElement('style');
        style.id = 'starStyle';
        style.textContent = `
            @keyframes starTwinkle {
                0%,100% { opacity: 0.1; transform: scale(1); }
                50% { opacity: 0.9; transform: scale(1.4); }
            }
        `;
        document.head.appendChild(style);
    }
}

// ── World Map Interaction ────────────────
function initMapInteraction() {
    const continents = document.querySelectorAll('.continent');
    const hotspots = document.querySelectorAll('.hotspot');
    const tooltip = document.getElementById('mapTooltip');
    const tooltipRegion = document.getElementById('tooltipRegion');
    const tooltipCount = document.getElementById('tooltipCount');

    const regionData = {
        'North America': '4,200+ experiences',
        'South America': '2,100+ experiences',
        'Europe': '5,800+ experiences',
        'Africa': '1,900+ experiences',
        'Asia': '6,400+ experiences',
        'Australia': '1,200+ experiences',
        'New York, USA': '380+ experiences',
        'Paris, France': '520+ experiences',
        'Tokyo, Japan': '440+ experiences',
        'Nairobi, Kenya': '210+ experiences',
        'Sydney, Australia': '290+ experiences',
        'Live: Climate Data': 'Real-time satellite feed',
    };

    const showTooltip = (el, name, e) => {
        tooltipRegion.textContent = name;
        tooltipCount.textContent = regionData[name] || '100+ experiences';
        tooltip.classList.add('visible');
        positionTooltip(e);
    };

    const positionTooltip = (e) => {
        const mapRect = document.querySelector('.map-globe-visual').getBoundingClientRect();
        const x = e.clientX - mapRect.left + 15;
        const y = e.clientY - mapRect.top - 30;
        tooltip.style.left = Math.min(x, mapRect.width - 200) + 'px';
        tooltip.style.top = Math.max(y, 10) + 'px';
    };

    const hideTooltip = () => tooltip.classList.remove('visible');

    continents.forEach(el => {
        el.addEventListener('mouseenter', (e) => showTooltip(el, el.dataset.region, e));
        el.addEventListener('mousemove', positionTooltip);
        el.addEventListener('mouseleave', hideTooltip);
        el.addEventListener('click', () => {
            alert(`Launching ${el.dataset.region} experiences! (Coming soon)`);
        });
    });

    hotspots.forEach(el => {
        el.addEventListener('mouseenter', (e) => showTooltip(el, el.querySelector('title').textContent, e));
        el.addEventListener('mousemove', positionTooltip);
        el.addEventListener('mouseleave', hideTooltip);
    });
}

// ── Map Filter Buttons ───────────────────
function initMapFilters() {
    const filters = document.querySelectorAll('.map-filter');
    filters.forEach(btn => {
        btn.addEventListener('click', () => {
            filters.forEach(f => f.classList.remove('active'));
            btn.classList.add('active');
        });
    });
}

// ── Tour Tabs ────────────────────────────
function initTourTabs() {
    const tabs = document.querySelectorAll('.tour-tab');
    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            tabs.forEach(t => t.classList.remove('active'));
            tab.classList.add('active');
            const grid = document.getElementById('toursGrid');
            grid.style.opacity = '0';
            grid.style.transform = 'translateY(15px)';
            setTimeout(() => {
                grid.style.transition = 'all 0.4s ease';
                grid.style.opacity = '1';
                grid.style.transform = 'translateY(0)';
            }, 50);
        });
    });
}

// ── Scroll-triggered Animations ──────────
function initScrollAnimations() {
    const targets = document.querySelectorAll(
        '.cat-card, .tour-card, .lab-card, .personal-card, .space-feat, .data-layer'
    );

    targets.forEach((el, i) => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(30px)';
        el.style.transition = `opacity 0.6s ease ${(i % 4) * 0.1}s, transform 0.6s ease ${(i % 4) * 0.1}s`;
    });

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.1, rootMargin: '0px 0px -40px 0px' });

    targets.forEach(el => observer.observe(el));
}