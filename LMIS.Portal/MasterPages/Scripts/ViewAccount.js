$(document).ready(function () {
    var session = '<%=Session["username"] != null ? "true" : "false"%>';
    $("#postNew").attr("href", "../IndividualRegistration/UniversityInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
    window.location.assign("../IndividualRegistration/ExperienceInformation.aspx?#anchor");
})
