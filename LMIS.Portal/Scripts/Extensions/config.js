var serverURL = window.location.href.toString().split(window.location.host)[0] + window.location.host;
var config = {
    individual: { photoPath: serverURL + '/Uploads/Individual/', defaultPhotoPath: serverURL + '/Uploads/Individual//default.png', photoFolder: '/Individual' },
    employer: {
        logoPath: serverURL + '/Uploads/', defaultLogoPath: serverURL + '/Uploads/Employer/Logos//default.png', logoFolder: '',
        profilePath: serverURL + '/Uploads/Employer/Profiles/', profileFolder: '/Employer/Profiles',
        authorizationletterPath: '/Uploads/Employer/AuthorizationLetters/', authorizationletterFolder: '/Employer/AuthorizationLetters'
    }
}