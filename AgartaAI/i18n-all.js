// ══════════════════════════════════════════════════════
// Agarta AI - Complete Site Translation System (EN/FR)
// ══════════════════════════════════════════════════════

const siteI18n = {

// ─── SHARED NAV & FOOTER ───
shared: {
  en: {
    "nav.features":"Features","nav.integrations":"Integrations","nav.about":"About",
    "nav.resources":"Resources","nav.support":"Support","nav.signup":"Sign Up / Login","nav.getstarted":"Get Started",
    "nav.f.explore":"Explore","nav.f.games":"Play & Games","nav.f.campus":"Virtual Campus","nav.f.events":"Events",
    "nav.f.marketplace":"Marketplace","nav.f.labs":"Lab Tours","nav.f.media":"Media Hub","nav.f.webnav":"Web Navigator",
    "nav.r.tech":"Technologies","nav.r.faq":"FAQs",
    "nav.s.help":"Help Center","nav.s.contact":"Contact Us","nav.s.legal":"Legal",
    "ft.company":"Company","ft.features":"Features","ft.resources":"Resources","ft.support":"Support","ft.legal":"Legal",
    "ft.about":"About Us","ft.story":"Our Story & Mission","ft.ethics":"Ethics & Sustainability","ft.careers":"Careers","ft.partnerships":"Partnerships",
    "ft.explore":"Explore","ft.games":"Play & Games","ft.campus":"Virtual Campus","ft.events":"Events","ft.marketplace":"Marketplace",
    "ft.blog":"Blog","ft.tutorials":"Tutorials & Webinars","ft.api":"Developer API Portal","ft.tech":"Technologies",
    "ft.guides":"Integration Guides","ft.community":"Community Forum","ft.faqs":"FAQs",
    "ft.contact":"Contact Us","ft.helpcenter":"Help Center","ft.aichat":"AI Assistant Chat (24/7)",
    "ft.privacy":"Privacy Policy","ft.terms":"Terms of Service","ft.dataethics":"Data & AI Ethics Policy",
    "ft.copyright":"Copyrights & Trademarks","ft.cookies":"Cookie Preferences",
    "ft.opendata":"Open Data Licensing","ft.envcomp":"Environmental Compliance",
    "ft.connect":"Connect with us:",
    "ft.bottom":"© 2026 Agarta AI | All rights reserved. | Designed with purpose and powered by Kimuntu Power Inc.",
    "auth.login.subtitle":"Enter your email and password to login",
    "auth.login.btn":"Login","auth.signup.subtitle":"Enter your email to sign up for this app",
    "auth.signup.btn":"Sign up with email","auth.or":"or continue with",
    "auth.noaccount":"Don't have an account?","auth.signuplink":"Sign up",
    "auth.hasaccount":"Already have an account?","auth.loginlink":"Login",
    "auth.terms":"By clicking continue, you agree to our","auth.tos":"Terms of Service","auth.pp":"Privacy Policy",
  },
  fr: {
    "nav.features":"Fonctionnalités","nav.integrations":"Intégrations","nav.about":"À propos",
    "nav.resources":"Ressources","nav.support":"Assistance","nav.signup":"Inscription / Connexion","nav.getstarted":"Commencer",
    "nav.f.explore":"Explorer","nav.f.games":"Jeux & Activités","nav.f.campus":"Campus Virtuel","nav.f.events":"Événements",
    "nav.f.marketplace":"Marché","nav.f.labs":"Visites de Labo","nav.f.media":"Hub Médias","nav.f.webnav":"Navigateur Web",
    "nav.r.tech":"Technologies","nav.r.faq":"FAQ",
    "nav.s.help":"Centre d'Aide","nav.s.contact":"Contactez-nous","nav.s.legal":"Légal",
    "ft.company":"Entreprise","ft.features":"Fonctionnalités","ft.resources":"Ressources","ft.support":"Assistance","ft.legal":"Légal",
    "ft.about":"À propos de nous","ft.story":"Notre Histoire & Mission","ft.ethics":"Éthique & Durabilité","ft.careers":"Carrières","ft.partnerships":"Partenariats",
    "ft.explore":"Explorer","ft.games":"Jeux & Activités","ft.campus":"Campus Virtuel","ft.events":"Événements","ft.marketplace":"Marché",
    "ft.blog":"Blog","ft.tutorials":"Tutoriels & Webinaires","ft.api":"Portail API Développeur","ft.tech":"Technologies",
    "ft.guides":"Guides d'Intégration","ft.community":"Forum Communautaire","ft.faqs":"FAQ",
    "ft.contact":"Contactez-nous","ft.helpcenter":"Centre d'Aide","ft.aichat":"Chat IA (24h/24)",
    "ft.privacy":"Politique de Confidentialité","ft.terms":"Conditions d'Utilisation","ft.dataethics":"Politique d'Éthique des Données & IA",
    "ft.copyright":"Droits d'Auteur & Marques","ft.cookies":"Préférences de Cookies",
    "ft.opendata":"Licences Open Data","ft.envcomp":"Conformité Environnementale",
    "ft.connect":"Suivez-nous :",
    "ft.bottom":"© 2026 Agarta AI | Tous droits réservés. | Conçu avec intention et propulsé par Kimuntu Power Inc.",
    "auth.login.subtitle":"Entrez votre courriel et mot de passe pour vous connecter",
    "auth.login.btn":"Connexion","auth.signup.subtitle":"Entrez votre courriel pour vous inscrire",
    "auth.signup.btn":"S'inscrire avec courriel","auth.or":"ou continuer avec",
    "auth.noaccount":"Vous n'avez pas de compte ?","auth.signuplink":"S'inscrire",
    "auth.hasaccount":"Vous avez déjà un compte ?","auth.loginlink":"Connexion",
    "auth.terms":"En cliquant continuer, vous acceptez nos","auth.tos":"Conditions d'Utilisation","auth.pp":"Politique de Confidentialité",
  }
},

// ─── ABOUT PAGE ───
about: {
  en: {
    "ab.hero.pre":"EXPERIENCE","ab.hero.h1":"THE FUTURE OF LEARNING",
    "ab.hero.p":"Welcome to Agarta, where technology, nature, and knowledge grow together. Explore the future of agriculture and the environment through AI-powered virtual tours and immersive experiences.",
    "ab.hero.btn1":"Explore the Platform","ab.hero.btn2":"Watch Our Story",
    "ab.vision.pre":"VISION &","ab.vision.h2":"MISSION",
    "ab.vision.title":"OUR VISION","ab.mission.title":"OUR MISSION",
    "ab.vision.p":"A next-generation platform uniting artificial intelligence, immersive 3D technology, and environmental education.",
    "ab.mission.p":"To transform the way individuals, communities, and institutions experience and understand agriculture and the environment by offering immersive, intelligent, and accessible digital experiences across all platforms.",
    "ab.power.pre":"POWERED BY","ab.power.h2":"INNOVATION",
    "ab.power.desc":"Combining AI, immersive environments, cross-platform accessibility, and smart integrations.",
    "ab.p1.title":"AI-POWERED EXPERIENCES","ab.p1.desc":"Personalized tours, voice-guided learning, and interactive simulations that adapt to every user.",
    "ab.p2.title":"IMMERSIVE ENVIRONMENTS","ab.p2.desc":"High-definition 3D and 360° HDR visuals with realistic soundscapes that transport you anywhere.",
    "ab.p3.title":"CROSS-PLATFORM ACCESSIBILITY","ab.p3.desc":"Fully compatible with computers, smartphones, tablets, and virtual reality devices.",
    "ab.p4.title":"SMART INTEGRATION","ab.p4.desc":"API connections with Google Earth, educational systems (LMS), and IoT data for real-time updates.",
    "ab.team.pre":"BORN FROM","ab.team.h2":"PASSION",
    "ab.cta.h2":"JOIN THE AGARTA MOVEMENT","ab.cta.p":"Be part of a global initiative connecting humans, nature, and intelligence.",
    "ab.cta.btn1":"Get Started","ab.cta.btn2":"Partner With Us",
  },
  fr: {
    "ab.hero.pre":"DÉCOUVREZ","ab.hero.h1":"LE FUTUR DE L'APPRENTISSAGE",
    "ab.hero.p":"Bienvenue chez Agarta, où technologie, nature et connaissance grandissent ensemble. Explorez l'avenir de l'agriculture et de l'environnement grâce à des visites virtuelles propulsées par l'IA.",
    "ab.hero.btn1":"Explorer la Plateforme","ab.hero.btn2":"Voir Notre Histoire",
    "ab.vision.pre":"VISION &","ab.vision.h2":"MISSION",
    "ab.vision.title":"NOTRE VISION","ab.mission.title":"NOTRE MISSION",
    "ab.vision.p":"Une plateforme de nouvelle génération unissant l'intelligence artificielle, la technologie 3D immersive et l'éducation environnementale.",
    "ab.mission.p":"Transformer la façon dont les individus, communautés et institutions vivent et comprennent l'agriculture et l'environnement en offrant des expériences numériques immersives, intelligentes et accessibles sur toutes les plateformes.",
    "ab.power.pre":"PROPULSÉ PAR","ab.power.h2":"L'INNOVATION",
    "ab.power.desc":"Combinant IA, environnements immersifs, accessibilité multi-plateforme et intégrations intelligentes.",
    "ab.p1.title":"EXPÉRIENCES PROPULSÉES PAR L'IA","ab.p1.desc":"Visites personnalisées, apprentissage guidé par la voix et simulations interactives adaptées à chaque utilisateur.",
    "ab.p2.title":"ENVIRONNEMENTS IMMERSIFS","ab.p2.desc":"Visuels 3D haute définition et HDR 360° avec des paysages sonores réalistes qui vous transportent partout.",
    "ab.p3.title":"ACCESSIBILITÉ MULTI-PLATEFORME","ab.p3.desc":"Entièrement compatible avec ordinateurs, smartphones, tablettes et appareils de réalité virtuelle.",
    "ab.p4.title":"INTÉGRATION INTELLIGENTE","ab.p4.desc":"Connexions API avec Google Earth, systèmes éducatifs (LMS) et données IoT pour des mises à jour en temps réel.",
    "ab.team.pre":"NÉ DE LA","ab.team.h2":"PASSION",
    "ab.cta.h2":"REJOIGNEZ LE MOUVEMENT AGARTA","ab.cta.p":"Faites partie d'une initiative mondiale reliant les humains, la nature et l'intelligence.",
    "ab.cta.btn1":"Commencer","ab.cta.btn2":"Devenez Partenaire",
  }
},

// ─── CONTACT PAGE ───
contact: {
  en: {
    "ct.hero.pre":"GET IN","ct.hero.h1":"TOUCH",
    "ct.hero.p":"Have a question, partnership opportunity, or just want to say hello? We'd love to hear from you.",
    "ct.form.title":"SEND US A MESSAGE","ct.form.desc":"Fill out the form below and our team will respond within 24 hours.",
    "ct.form.name":"Full Name","ct.form.email":"Email Address","ct.form.subject":"Subject","ct.form.message":"Your Message","ct.form.btn":"Send Message →",
    "ct.email.title":"EMAIL","ct.email.desc":"General inquiries and information",
    "ct.support.title":"SUPPORT","ct.support.desc":"Technical help and customer service",
    "ct.partner.title":"PARTNERSHIPS","ct.partner.desc":"Institutional and business partnerships",
    "ct.offices.pre":"OUR","ct.offices.h2":"OFFICES",
    "ct.offices.desc":"Regional offices supporting learners, educators, and partners worldwide.",
    "ct.o1.title":"CANADA (HQ)","ct.o2.title":"UNITED STATES","ct.o3.title":"FRANCE",
    "ct.social.pre":"STAY","ct.social.h2":"CONNECTED",
    "ct.social.desc":"Join our community on social media for updates, tips, and behind-the-scenes content.",
    "ct.map.pre":"FIND US ON THE","ct.map.h2":"MAP",
  },
  fr: {
    "ct.hero.pre":"ENTREZ EN","ct.hero.h1":"CONTACT",
    "ct.hero.p":"Vous avez une question, une opportunité de partenariat ou simplement envie de dire bonjour ? Nous serions ravis de vous entendre.",
    "ct.form.title":"ENVOYEZ-NOUS UN MESSAGE","ct.form.desc":"Remplissez le formulaire ci-dessous et notre équipe répondra sous 24 heures.",
    "ct.form.name":"Nom complet","ct.form.email":"Adresse courriel","ct.form.subject":"Objet","ct.form.message":"Votre message","ct.form.btn":"Envoyer le Message →",
    "ct.email.title":"COURRIEL","ct.email.desc":"Demandes générales et informations",
    "ct.support.title":"ASSISTANCE","ct.support.desc":"Aide technique et service client",
    "ct.partner.title":"PARTENARIATS","ct.partner.desc":"Partenariats institutionnels et commerciaux",
    "ct.offices.pre":"NOS","ct.offices.h2":"BUREAUX",
    "ct.offices.desc":"Bureaux régionaux au service des apprenants, éducateurs et partenaires du monde entier.",
    "ct.o1.title":"CANADA (SIÈGE)","ct.o2.title":"ÉTATS-UNIS","ct.o3.title":"FRANCE",
    "ct.social.pre":"RESTEZ","ct.social.h2":"CONNECTÉ",
    "ct.social.desc":"Rejoignez notre communauté sur les réseaux sociaux pour des mises à jour et du contenu exclusif.",
    "ct.map.pre":"TROUVEZ-NOUS SUR LA","ct.map.h2":"CARTE",
  }
},

// ─── EVENTS PAGE ───
events: {
  en: {
    "ev.hero.h1":"EXPERIENCE THE WORLD","ev.hero.h1b":"THROUGH EVENTS",
    "ev.hero.p":"Join live virtual field trips, conferences, workshops, and immersive cultural experiences powered by Agarta AI.",
    "ev.upcoming":"DISCOVER","ev.upcoming2":"UPCOMING EVENTS",
    "ev.live.pre":"JOIN","ev.live.h2":"LIVE NOW",
    "ev.past.pre":"RELIVE PAST","ev.past.h2":"EXPERIENCES",
    "ev.cta.h2":"NEVER MISS AN EVENT","ev.cta.p":"Subscribe to get notified about upcoming events and exclusive experiences.",
    "ev.cta.btn":"Subscribe to Events",
  },
  fr: {
    "ev.hero.h1":"VIVEZ LE MONDE","ev.hero.h1b":"À TRAVERS LES ÉVÉNEMENTS",
    "ev.hero.p":"Participez à des excursions virtuelles en direct, conférences, ateliers et expériences culturelles immersives propulsées par Agarta AI.",
    "ev.upcoming":"DÉCOUVREZ","ev.upcoming2":"LES ÉVÉNEMENTS À VENIR",
    "ev.live.pre":"REJOIGNEZ","ev.live.h2":"EN DIRECT",
    "ev.past.pre":"REVIVEZ LES","ev.past.h2":"EXPÉRIENCES PASSÉES",
    "ev.cta.h2":"NE MANQUEZ AUCUN ÉVÉNEMENT","ev.cta.p":"Abonnez-vous pour être notifié des événements à venir et des expériences exclusives.",
    "ev.cta.btn":"S'abonner aux Événements",
  }
},

// ─── FAQ PAGE ───
faq: {
  en: {
    "fq.title":"FREQUENTLY ASKED QUESTIONS",
    "fq.search":"Search for answers...",
    "fq.c1":"GENERAL QUESTIONS","fq.c2":"EDUCATION & LEARNING","fq.c3":"TECHNOLOGY & FEATURES",
    "fq.c4":"INTEGRATIONS & CONTENT","fq.c5":"PRICING & ACCESS","fq.c6":"SECURITY & PRIVACY","fq.c7":"SUPPORT & HELP",
    "fq.noresult":"No matching questions found",
    "fq.cta.title":"STILL HAVE QUESTIONS?","fq.cta.p":"Our team is here to help. Reach out through any of these channels.",
    "fq.cta.btn1":"Contact Support","fq.cta.btn2":"Live Chat","fq.cta.btn3":"Email Us",
  },
  fr: {
    "fq.title":"FOIRE AUX QUESTIONS",
    "fq.search":"Rechercher des réponses...",
    "fq.c1":"QUESTIONS GÉNÉRALES","fq.c2":"ÉDUCATION & APPRENTISSAGE","fq.c3":"TECHNOLOGIE & FONCTIONNALITÉS",
    "fq.c4":"INTÉGRATIONS & CONTENU","fq.c5":"TARIFS & ACCÈS","fq.c6":"SÉCURITÉ & CONFIDENTIALITÉ","fq.c7":"ASSISTANCE & AIDE",
    "fq.noresult":"Aucune question correspondante trouvée",
    "fq.cta.title":"ENCORE DES QUESTIONS ?","fq.cta.p":"Notre équipe est là pour vous aider. Contactez-nous via l'un de ces canaux.",
    "fq.cta.btn1":"Contacter le Support","fq.cta.btn2":"Chat en Direct","fq.cta.btn3":"Envoyez un Courriel",
  }
},

// ─── HELP & SUPPORT PAGE ───
help: {
  en: {
    "hl.hero.pre":"HOW CAN WE","hl.hero.h1":"HELP YOU?",
    "hl.hero.search":"Search for help articles, tutorials, FAQs...",
    "hl.cat.pre":"BROWSE BY","hl.cat.h2":"CATEGORY",
    "hl.c1":"GETTING STARTED","hl.c2":"TROUBLESHOOTING","hl.c3":"ACCOUNT & SETTINGS","hl.c4":"BILLING & PLANS",
    "hl.video.pre":"WATCH &","hl.video.h2":"LEARN",
    "hl.guide.pre":"IN-DEPTH","hl.guide.h2":"GUIDES",
    "hl.cta.h2":"STILL NEED HELP?","hl.cta.p":"Our support team is available 24/7 to assist you.",
    "hl.cta.btn1":"Contact Support","hl.cta.btn2":"Live Chat","hl.cta.btn3":"Community Forum",
  },
  fr: {
    "hl.hero.pre":"COMMENT POUVONS-NOUS","hl.hero.h1":"VOUS AIDER ?",
    "hl.hero.search":"Rechercher des articles d'aide, tutoriels, FAQ...",
    "hl.cat.pre":"PARCOURIR PAR","hl.cat.h2":"CATÉGORIE",
    "hl.c1":"PREMIERS PAS","hl.c2":"DÉPANNAGE","hl.c3":"COMPTE & PARAMÈTRES","hl.c4":"FACTURATION & FORFAITS",
    "hl.video.pre":"REGARDEZ &","hl.video.h2":"APPRENEZ",
    "hl.guide.pre":"GUIDES","hl.guide.h2":"APPROFONDIS",
    "hl.cta.h2":"BESOIN D'AIDE ?","hl.cta.p":"Notre équipe de support est disponible 24h/24 pour vous aider.",
    "hl.cta.btn1":"Contacter le Support","hl.cta.btn2":"Chat en Direct","hl.cta.btn3":"Forum Communautaire",
  }
},

// ─── LEGAL PAGE ───
legal: {
  en: {
    "lg.hero.pre":"LEGAL","lg.hero.h1":"CENTER",
    "lg.tab1":"Privacy Policy","lg.tab2":"Terms & Conditions","lg.tab3":"Cookie Policy","lg.tab4":"AI Ethics",
    "lg.pp.title":"PRIVACY POLICY","lg.tc.title":"TERMS & CONDITIONS",
    "lg.ck.title":"COOKIE POLICY","lg.ai.title":"AI ETHICS POLICY",
  },
  fr: {
    "lg.hero.pre":"CENTRE","lg.hero.h1":"LÉGAL",
    "lg.tab1":"Politique de Confidentialité","lg.tab2":"Conditions Générales","lg.tab3":"Politique de Cookies","lg.tab4":"Éthique IA",
    "lg.pp.title":"POLITIQUE DE CONFIDENTIALITÉ","lg.tc.title":"CONDITIONS GÉNÉRALES",
    "lg.ck.title":"POLITIQUE DE COOKIES","lg.ai.title":"POLITIQUE D'ÉTHIQUE IA",
  }
},

// ─── MARKETPLACE PAGE ───
marketplace: {
  en: {
    "mk.hero.h1":"UNLOCK KNOWLEDGE, PLAY, AND","mk.hero.h1b":"EXPLORE",
    "mk.hero.p":"Discover AI-powered courses, immersive games, avatars, and virtual experiences in the Agarta Marketplace.",
    "mk.featured":"FEATURED","mk.featured2":"COLLECTIONS",
    "mk.courses":"PURCHASE","mk.courses2":"COURSES",
    "mk.avatars":"PURCHASE","mk.avatars2":"AVATARS & ITEMS",
    "mk.cta.h2":"START YOUR LEARNING ADVENTURE","mk.cta.btn":"Browse All Products",
  },
  fr: {
    "mk.hero.h1":"DÉBLOQUEZ CONNAISSANCES, JEUX ET","mk.hero.h1b":"EXPLORATION",
    "mk.hero.p":"Découvrez des cours propulsés par l'IA, des jeux immersifs, des avatars et des expériences virtuelles sur le Marché Agarta.",
    "mk.featured":"COLLECTIONS","mk.featured2":"EN VEDETTE",
    "mk.courses":"ACHETER DES","mk.courses2":"COURS",
    "mk.avatars":"ACHETER DES","mk.avatars2":"AVATARS & OBJETS",
    "mk.cta.h2":"COMMENCEZ VOTRE AVENTURE D'APPRENTISSAGE","mk.cta.btn":"Parcourir Tous les Produits",
  }
},

// ─── TECHNOLOGIES PAGE ───
technologies: {
  en: {
    "tc.hero.title":"TECHNOLOGIES",
    "tc.hero.desc":"Agarta AI is built on a powerful combination of artificial intelligence, immersive technologies, real-world data, and secure cloud infrastructure.",
    "tc.s1.title":"AI ENGINE & INTELLIGENT SYSTEMS",
    "tc.s2.title":"XR TECHNOLOGIES (VR / AR / MR)",
    "tc.s3.title":"MAPPING, EARTH & SPACE DATA",
    "tc.s4.title":"CONTENT CREATION & SIMULATION",
    "tc.s5.title":"INFRASTRUCTURE, CLOUD & SECURITY",
    "tc.future.title":"FUTURE INTEGRATIONS",
    "tc.cta.title":"BUILD WITH AGARTA","tc.cta.p":"Join us in building the next generation of immersive, intelligent experiences.",
    "tc.cta.btn1":"Partner With Us","tc.cta.btn2":"Developer Portal","tc.cta.btn3":"Contact Team",
  },
  fr: {
    "tc.hero.title":"TECHNOLOGIES",
    "tc.hero.desc":"Agarta AI est construit sur une combinaison puissante d'intelligence artificielle, de technologies immersives, de données réelles et d'infrastructure cloud sécurisée.",
    "tc.s1.title":"MOTEUR IA & SYSTÈMES INTELLIGENTS",
    "tc.s2.title":"TECHNOLOGIES XR (VR / AR / MR)",
    "tc.s3.title":"CARTOGRAPHIE, DONNÉES TERRESTRES & SPATIALES",
    "tc.s4.title":"CRÉATION DE CONTENU & SIMULATION",
    "tc.s5.title":"INFRASTRUCTURE, CLOUD & SÉCURITÉ",
    "tc.future.title":"INTÉGRATIONS FUTURES",
    "tc.cta.title":"CONSTRUISEZ AVEC AGARTA","tc.cta.p":"Rejoignez-nous pour construire la prochaine génération d'expériences immersives et intelligentes.",
    "tc.cta.btn1":"Devenez Partenaire","tc.cta.btn2":"Portail Développeur","tc.cta.btn3":"Contacter l'Équipe",
  }
},

// ─── EXPLORE PAGE ───
explore: {
  en: {
    "ex.hero.h1":"Explore the World in Every Dimension",
    "ex.hero.p":"Navigate Earth's most incredible destinations through AI-powered virtual tours, 360° experiences, and immersive simulations.",
    "ex.hub.title":"Your Global Navigation Hub",
    "ex.cat.title":"Explore by Category",
    "ex.c1":"Cities & Countries","ex.c2":"Nature & Ecosystems","ex.c3":"Museums & History","ex.c4":"Space & Universe",
    "ex.featured":"Featured & Trending Tours",
    "ex.beyond":"Journey Beyond",
    "ex.cta.h2":"START YOUR EXPLORATION","ex.cta.btn":"Launch Explorer",
  },
  fr: {
    "ex.hero.h1":"Explorez le Monde dans Toutes ses Dimensions",
    "ex.hero.p":"Naviguez vers les destinations les plus incroyables de la Terre grâce à des visites virtuelles propulsées par l'IA, des expériences 360° et des simulations immersives.",
    "ex.hub.title":"Votre Centre de Navigation Mondial",
    "ex.cat.title":"Explorer par Catégorie",
    "ex.c1":"Villes & Pays","ex.c2":"Nature & Écosystèmes","ex.c3":"Musées & Histoire","ex.c4":"Espace & Univers",
    "ex.featured":"Visites en Vedette & Tendances",
    "ex.beyond":"Voyage au-delà",
    "ex.cta.h2":"COMMENCEZ VOTRE EXPLORATION","ex.cta.btn":"Lancer l'Explorateur",
  }
},

// ─── PLAY & GAMES PAGE ───
games: {
  en: {
    "gm.hero.h1":"PLAY, LEARN &","gm.hero.h1b":"EXPLORE",
    "gm.hero.p":"AI-powered educational games, VR adventures, and interactive challenges designed to make learning fun.",
    "gm.genre.pre":"CHOOSE YOUR","gm.genre.h2":"ADVENTURE",
    "gm.c1":"EDUCATIONAL GAMES","gm.c2":"VR ADVENTURES","gm.c3":"AI CHALLENGES","gm.c4":"MULTIPLAYER QUESTS",
    "gm.ai.pre":"PLAY WITH","gm.ai.h2":"AI",
    "gm.cta.h2":"READY TO PLAY?","gm.cta.btn":"Launch Game Hub",
  },
  fr: {
    "gm.hero.h1":"JOUEZ, APPRENEZ &","gm.hero.h1b":"EXPLOREZ",
    "gm.hero.p":"Jeux éducatifs propulsés par l'IA, aventures VR et défis interactifs conçus pour rendre l'apprentissage amusant.",
    "gm.genre.pre":"CHOISISSEZ VOTRE","gm.genre.h2":"AVENTURE",
    "gm.c1":"JEUX ÉDUCATIFS","gm.c2":"AVENTURES VR","gm.c3":"DÉFIS IA","gm.c4":"QUÊTES MULTIJOUEUR",
    "gm.ai.pre":"JOUEZ AVEC","gm.ai.h2":"L'IA",
    "gm.cta.h2":"PRÊT À JOUER ?","gm.cta.btn":"Lancer le Hub de Jeux",
  }
},

// ─── LAB TOURS PAGE ───
labTours: {
  en: {
    "lt.hero.pre":"INSIDE THE WORLD OF","lt.hero.h1":"AGARTA LABS",
    "lt.hero.p":"Step behind the scenes of our innovation labs and explore how we capture, create, and deliver immersive experiences.",
    "lt.real.pre":"REAL WORLD","lt.real.h2":"CAPTURES",
    "lt.explore.pre":"EXPLORE OUR","lt.explore.h2":"LABS",
    "lt.c1":"NATURE & ECOSYSTEMS","lt.c2":"FARMS & AGRICULTURE","lt.c3":"MUSEUMS & CULTURE",
    "lt.c4":"HOMES & LIVING SPACES","lt.c5":"SCHOOLS & INSTITUTIONS","lt.c6":"VIRTUAL TOURS & PUBLIC SPACES",
    "lt.pipeline.pre":"FROM CAPTURE TO","lt.pipeline.h2":"EXPERIENCE",
    "lt.output.pre":"WHAT USERS CAN","lt.output.h2":"EXPERIENCE",
    "lt.cta.h2":"EXPLORE THE LABS","lt.cta.btn":"Start a Lab Tour",
  },
  fr: {
    "lt.hero.pre":"AU CŒUR DU MONDE","lt.hero.h1":"DES LABOS AGARTA",
    "lt.hero.p":"Découvrez les coulisses de nos laboratoires d'innovation et explorez comment nous capturons, créons et offrons des expériences immersives.",
    "lt.real.pre":"CAPTURES DU","lt.real.h2":"MONDE RÉEL",
    "lt.explore.pre":"EXPLOREZ NOS","lt.explore.h2":"LABORATOIRES",
    "lt.c1":"NATURE & ÉCOSYSTÈMES","lt.c2":"FERMES & AGRICULTURE","lt.c3":"MUSÉES & CULTURE",
    "lt.c4":"MAISONS & ESPACES DE VIE","lt.c5":"ÉCOLES & INSTITUTIONS","lt.c6":"VISITES VIRTUELLES & ESPACES PUBLICS",
    "lt.pipeline.pre":"DE LA CAPTURE À","lt.pipeline.h2":"L'EXPÉRIENCE",
    "lt.output.pre":"CE QUE LES UTILISATEURS PEUVENT","lt.output.h2":"VIVRE",
    "lt.cta.h2":"EXPLOREZ LES LABOS","lt.cta.btn":"Commencer une Visite de Labo",
  }
},

// ─── VIRTUAL CAMPUS PAGE ───
campus: {
  en: {
    "vc.hero.h1":"INTERACTIVE VIRTUAL","vc.hero.h1b":"CAMPUS",
    "vc.hero.p":"Experience next-generation education through AI-powered virtual classrooms, XR labs, and immersive learning environments.",
    "vc.labs.pre":"XR LABS —","vc.labs.h2":"HANDS-ON LEARNING",
    "vc.c1":"Science Labs","vc.c2":"Engineering & Tech","vc.c3":"Health & Medical","vc.c4":"Environmental","vc.c5":"Agriculture",
    "vc.class.pre":"AGARTA VIRTUAL","vc.class.h2":"CLASSROOMS",
    "vc.sub1":"SCIENCE & STEM","vc.sub2":"ARTS & CULTURE","vc.sub3":"TECHNOLOGY & AI",
    "vc.sub4":"HEALTH & LIFE SCIENCES","vc.sub5":"AGRICULTURE & ENVIRONMENT","vc.sub6":"GEOGRAPHY & SOCIAL STUDIES",
    "vc.cta.h2":"JOIN THE VIRTUAL CAMPUS","vc.cta.btn":"Enroll Now",
  },
  fr: {
    "vc.hero.h1":"CAMPUS VIRTUEL","vc.hero.h1b":"INTERACTIF",
    "vc.hero.p":"Vivez l'éducation de nouvelle génération grâce à des salles de classe virtuelles propulsées par l'IA, des laboratoires XR et des environnements d'apprentissage immersifs.",
    "vc.labs.pre":"LABOS XR —","vc.labs.h2":"APPRENTISSAGE PRATIQUE",
    "vc.c1":"Laboratoires Scientifiques","vc.c2":"Ingénierie & Tech","vc.c3":"Santé & Médical","vc.c4":"Environnement","vc.c5":"Agriculture",
    "vc.class.pre":"SALLES DE CLASSE","vc.class.h2":"VIRTUELLES AGARTA",
    "vc.sub1":"SCIENCES & STEM","vc.sub2":"ARTS & CULTURE","vc.sub3":"TECHNOLOGIE & IA",
    "vc.sub4":"SANTÉ & SCIENCES DE LA VIE","vc.sub5":"AGRICULTURE & ENVIRONNEMENT","vc.sub6":"GÉOGRAPHIE & ÉTUDES SOCIALES",
    "vc.cta.h2":"REJOIGNEZ LE CAMPUS VIRTUEL","vc.cta.btn":"S'inscrire Maintenant",
  }
},

// ─── INTEGRATIONS PAGE ───
integrations: {
  en: {
    "ig.hero.title":"INTEGRATIONS",
    "ig.hero.desc":"An open ecosystem built for infinite possibilities. Agarta integrates world-class mapping, satellite, media, hardware, and API technologies to deliver trusted, real-time, immersive experiences.",
    "ig.hero.btn1":"Explore Integrations","ig.hero.btn2":"Become a Technology Partner",
    "ig.why.title":"POWERING REAL-WORLD EXPERIENCES",
    "ig.why.desc":"Agarta is designed as an open, modular platform. By integrating industry-leading technologies, we ensure reliability, scalability, and continuous innovation.",
    "ig.s2.title":"MAPPING & GEOSPATIAL PLATFORMS",
    "ig.s3.title":"SATELLITE & EARTH OBSERVATION DATA",
    "ig.s4.title":"MEDIA & STREAMING PLATFORMS",
    "ig.s5.title":"HARDWARE & CAPTURE DEVICES",
    "ig.s6.title":"APIs & DEVELOPER INTEGRATIONS",
    "ig.s7.title":"SECURITY & COMPLIANCE",
    "ig.future.title":"FUTURE INTEGRATIONS",
    "ig.future.desc":"Agarta continuously expands its ecosystem to include advanced AI models, emerging XR hardware, educational platforms, and government smart-city systems.",
    "ig.cta.title":"BUILD THE FUTURE WITH AGARTA",
    "ig.cta.desc":"Partner with us to shape the next generation of immersive, intelligent experiences.",
    "ig.cta.btn1":"Become an Integration Partner","ig.cta.btn2":"Request Technical Documentation","ig.cta.btn3":"Contact Our Team",
  },
  fr: {
    "ig.hero.title":"INTÉGRATIONS",
    "ig.hero.desc":"Un écosystème ouvert conçu pour des possibilités infinies. Agarta intègre des technologies de cartographie, satellite, médias, matériel et API de classe mondiale pour offrir des expériences immersives fiables en temps réel.",
    "ig.hero.btn1":"Explorer les Intégrations","ig.hero.btn2":"Devenir Partenaire Technologique",
    "ig.why.title":"DES EXPÉRIENCES RÉELLES PROPULSÉES",
    "ig.why.desc":"Agarta est conçu comme une plateforme ouverte et modulaire. En intégrant des technologies de pointe, nous garantissons fiabilité, évolutivité et innovation continue.",
    "ig.s2.title":"PLATEFORMES CARTOGRAPHIQUES & GÉOSPATIALES",
    "ig.s3.title":"DONNÉES SATELLITE & OBSERVATION TERRESTRE",
    "ig.s4.title":"PLATEFORMES MÉDIAS & STREAMING",
    "ig.s5.title":"MATÉRIEL & DISPOSITIFS DE CAPTURE",
    "ig.s6.title":"APIs & INTÉGRATIONS DÉVELOPPEURS",
    "ig.s7.title":"SÉCURITÉ & CONFORMITÉ",
    "ig.future.title":"INTÉGRATIONS FUTURES",
    "ig.future.desc":"Agarta élargit continuellement son écosystème pour inclure des modèles IA avancés, du matériel XR émergent, des plateformes éducatives et des systèmes de villes intelligentes.",
    "ig.cta.title":"CONSTRUISEZ L'AVENIR AVEC AGARTA",
    "ig.cta.desc":"Associez-vous à nous pour façonner la prochaine génération d'expériences immersives et intelligentes.",
    "ig.cta.btn1":"Devenir Partenaire d'Intégration","ig.cta.btn2":"Demander la Documentation Technique","ig.cta.btn3":"Contacter Notre Équipe",
  }
},

// ─── WEB NAVIGATOR PAGE ───
webNavigator: {
  en: {
    "wn.hero.title":"WEB NAVIGATOR",
    "wn.hero.desc":"Explore the web safely inside Agarta. Access trusted websites, learning portals, research tools, and email services — all in one secure environment.",
    "wn.hero.btn1":"Open Web Navigator","wn.hero.btn2":"View Trusted Partners",
    "wn.s1.title":"EMBEDDED SECURE BROWSER","wn.s2.title":"TRUSTED PARTNER WEBSITES",
    "wn.s3.title":"LEARNING RESOURCES HUB","wn.s4.title":"WEB RESEARCH TOOLS",
    "wn.s5.title":"EMAIL ACCESS (SECURE WEBMAIL)","wn.s6.title":"BOOKMARKS & FAVORITES",
    "wn.s7.title":"AI ASSISTANCE PANEL","wn.s8.title":"ACCESS CONTROL & USER PROFILES",
    "wn.eco.title":"INTEGRATION WITH AGARTA","wn.why.title":"WHY WEB NAVIGATOR MATTERS",
    "wn.cta.title":"START EXPLORING SAFELY","wn.cta.btn1":"Get Started","wn.cta.btn2":"Contact Our Team",
  },
  fr: {
    "wn.hero.title":"NAVIGATEUR WEB",
    "wn.hero.desc":"Explorez le web en toute sécurité dans Agarta. Accédez à des sites de confiance, des portails d'apprentissage, des outils de recherche et des services de messagerie — le tout dans un environnement sécurisé.",
    "wn.hero.btn1":"Ouvrir le Navigateur Web","wn.hero.btn2":"Voir les Partenaires",
    "wn.s1.title":"NAVIGATEUR SÉCURISÉ INTÉGRÉ","wn.s2.title":"SITES WEB PARTENAIRES DE CONFIANCE",
    "wn.s3.title":"CENTRE DE RESSOURCES D'APPRENTISSAGE","wn.s4.title":"OUTILS DE RECHERCHE WEB",
    "wn.s5.title":"ACCÈS COURRIEL (WEBMAIL SÉCURISÉ)","wn.s6.title":"SIGNETS & FAVORIS",
    "wn.s7.title":"PANNEAU D'ASSISTANCE IA","wn.s8.title":"CONTRÔLE D'ACCÈS & PROFILS UTILISATEURS",
    "wn.eco.title":"INTÉGRATION AVEC AGARTA","wn.why.title":"POURQUOI LE NAVIGATEUR WEB COMPTE",
    "wn.cta.title":"COMMENCEZ À EXPLORER EN SÉCURITÉ","wn.cta.btn1":"Commencer","wn.cta.btn2":"Contacter Notre Équipe",
  }
},

// ─── MEDIA & STREAMING HUB ───
mediaStreaming: {
  en: {
    "ms.hero.title":"MEDIA & STREAMING HUB",
    "ms.hero.desc":"Watch. Explore. Experience the world through immersive media.",
    "ms.hero.btn1":"▶️ Watch Now","ms.hero.btn2":"🌍 Explore 360°","ms.hero.btn3":"🔴 Live Streams",
    "ms.s1.title":"FEATURED VIDEOS","ms.s2.title":"LIVE STREAMS","ms.s3.title":"360° & VR CONTENT",
    "ms.s4.title":"PARTNER PLATFORMS","ms.s5.title":"EDUCATIONAL MEDIA ZONE",
    "ms.ai.title":"AGARTA AI FEATURES","ms.s7.title":"PERSONALIZED EXPERIENCE",
    "ms.s8.title":"SAFETY, RIGHTS & COMPLIANCE","ms.scale.title":"SCALABILITY & FUTURE",
    "ms.cta.title":"START WATCHING DIFFERENTLY","ms.cta.btn1":"Join Agarta","ms.cta.btn2":"Explore 360°","ms.cta.btn3":"🔴 Watch Live",
  },
  fr: {
    "ms.hero.title":"HUB MÉDIAS & STREAMING",
    "ms.hero.desc":"Regardez. Explorez. Vivez le monde à travers des médias immersifs.",
    "ms.hero.btn1":"▶️ Regarder","ms.hero.btn2":"🌍 Explorer 360°","ms.hero.btn3":"🔴 En Direct",
    "ms.s1.title":"VIDÉOS EN VEDETTE","ms.s2.title":"DIFFUSIONS EN DIRECT","ms.s3.title":"CONTENU 360° & VR",
    "ms.s4.title":"PLATEFORMES PARTENAIRES","ms.s5.title":"ZONE MÉDIAS ÉDUCATIFS",
    "ms.ai.title":"FONCTIONNALITÉS IA D'AGARTA","ms.s7.title":"EXPÉRIENCE PERSONNALISÉE",
    "ms.s8.title":"SÉCURITÉ, DROITS & CONFORMITÉ","ms.scale.title":"ÉVOLUTIVITÉ & FUTUR",
    "ms.cta.title":"REGARDEZ AUTREMENT","ms.cta.btn1":"Rejoindre Agarta","ms.cta.btn2":"Explorer 360°","ms.cta.btn3":"🔴 Regarder en Direct",
  }
},

};

// ─── TRANSLATION ENGINE ───
function applyTranslation(lang) {
  const t = {};
  // Merge all page dictionaries into one flat lookup
  for (const page of Object.values(siteI18n)) {
    if (page[lang]) Object.assign(t, page[lang]);
  }
  document.querySelectorAll('[data-i18n]').forEach(el => {
    const key = el.getAttribute('data-i18n');
    if (t[key] !== undefined) {
      if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') {
        el.placeholder = t[key];
      } else {
        el.textContent = t[key];
      }
    }
  });
  document.documentElement.lang = lang;
  document.querySelectorAll('.lang-switch-btn').forEach(btn => {
    btn.classList.toggle('active', btn.dataset.lang === lang);
  });
  const slider = document.querySelector('.switch-slider');
  if (slider) slider.style.transform = lang === 'fr' ? 'translateX(100%)' : 'translateX(0)';
  localStorage.setItem('agarta_lang', lang);
}

// Run immediately (DOM is already ready when this script loads at end of body)
(function() {
  const saved = localStorage.getItem('agarta_lang') || 'en';
  if (saved === 'fr') applyTranslation('fr');
  // Override ALL existing lang-switch handlers by cloning buttons
  document.querySelectorAll('.lang-switch-btn').forEach(btn => {
    const newBtn = btn.cloneNode(true);
    btn.parentNode.replaceChild(newBtn, btn);
    newBtn.addEventListener('click', () => {
      applyTranslation(newBtn.dataset.lang);
    });
  });
})();
