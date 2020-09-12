function login() {

    var dto = { email: $(".email").val(), password: $(".password").val() };
    
    lmis.ajax("../Administration/login.aspx/chklogin", dto, 0, "show,close",
    function (data) {
        if (data) {
            switch (data.d.status) {
                case 1:
                 
                     lmis.notification.success(data.d.Message);
                     window.location.href = '/home';
                     break;
           
                default:
                    lmis.notification.error(data.d.Message);
                
                    break;
            }
      
        }
        //lmis.ajaxSuccessHandler("Success");
    });



}