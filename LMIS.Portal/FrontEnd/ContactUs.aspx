<%@ Page Title="<%$ Resources:CommonControls,F034_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="LMIS.Portal.FrontEnd.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/star-rating.css" rel="stylesheet" />
     <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
   <script src="../FrontEnd/Scripts/ContactUs.js" async="async"></script>
   <script src="../js/star-rating.js" type="text/javascript"></script>
         <div class="container no-padding">
        <div class="gmap-area">
            <div class="container">
                <div class="row">
                    <div class="col-sm-5 text-center">
                        <div class="gmap">
                            <iframe id="gmapIframe" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"   ></iframe>
                        </div>
                    </div>

                    <div class="col-sm-7 map-content">
                        <ul class="row">
                            <li class="col-sm-6">
                                 <h1>Offices:</h1>
                                <address data-bind="foreach: Offices">
                                 <div data-bind="html: Value"></div>
                                </address>

                            </li>


                            <li class="col-sm-6">
                                <h1>Emails:</h1>
                                 <address data-bind="foreach: Emails">
                                    <p><strong data-bind="text: Title"></strong>:<a data-bind="    attr: { href: 'mailto:'  + EmailAddress() }"> <span data-bind="    text: EmailAddress"></span></a></p>
                                    
                                
                                </address>

                          
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        </div>
      <!--/gmap_area -->

        <div class="container white-bg">
         <div class="container">
       
     <div class="col-lg-2"></div>     
  <div class="col-lg-10">
          <div id="carousel-slider" class="carousel slide" data-ride="carousel">
					<!-- Indicators -->
				  	<ol class="carousel-indicators visible-xs">
					    <li data-target="#carousel-slider" data-slide-to="0" class="active"></li>
					    <li data-target="#carousel-slider" data-slide-to="1"></li>
					    <li data-target="#carousel-slider" data-slide-to="2"></li>
				  	</ol>

					<div class="carousel-inner" data-bind="foreach: TestimonialList" >
						<div class="item" data-bind="css:  {'active' : $index()==0}">
							<div class="clients-comments text-center">
                       
                        <div class="col-lg-10"><h3 data-bind="text: Comment"></h3>
                        <h4 data-bind="text: Name"> </h4></div>
                    </div>
					   </div>
					 
					</div>
					
					<a class="left carousel-control hidden-xs" href="#carousel-slider" data-slide="prev">
						<i class="fa fa-angle-left"></i> 
					</a>
					
					<a class=" right carousel-control hidden-xs"href="#carousel-slider" data-slide="next">
						<i class="fa fa-angle-right"></i> 
					</a>
				</div>
          </div>
            </div> 
			
         
               <div class="container">        
                <h2><%=GetGlobalResourceObject("CommonControls", "F034_YourMessage")%> </h2>
               
            </div> 
           
             
          
               
            <div class="row contact-wrap"> 
                <div class="status alert alert-success" style="display: none"></div>
                <form id="main-contact-form" class="contact-form" name="contact-form" method="post" action="sendemail.php">
             <div name="isTestinomial" class="col-sm-10 col-sm-offset-1 isTestinomial">
            
                  <input type="radio" name="feddback" value="feeddBack" onchange="return isFeedBack(this);" class="Testinomial"> 
                   <%=GetGlobalResourceObject("CommonControls", "F034_feeddBack")%>
               <br>
             
             <input type="radio" name="feddback" value="Testinomial" onchange="return isFeedBack(this);"  class="Testinomial"  />
            <%=GetGlobalResourceObject("CommonControls", "F034_Testimonial")%>
             </div>   
                      
                    <div class="col-sm-5 col-sm-offset-1">
                         <div class="form-group ">
                <label class="lblFeedBack" ><%=GetGlobalResourceObject("CommonControls", "F034_TypeFeedBack")%> *</label>
                <select required  class="form-control always-white ddlFeedBack validationElement" data-bind="options: FeedBackOptions, value: FeedBackType, optionsValue: 'id', optionsText: 'desc',optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    ' ">
                </select>
            </div>
                        <div id="divRating">
                            <div class="siteRating" style="display:none">
                                <label ><%=GetGlobalResourceObject("CommonControls", "F034_SiteRating")%>  *</label>
                                <input id="input-21f" class="txtsiteRating validationElement" value="0" data-size="xs" data-symbol="*" >
                            </div>
                        </div>
                        <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "X_Email")%>: </label>
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </div>
                      
                       <!-- <div class="form-group">
                            <label>Phone</label>
                            <input type="number" class="form-control">
                        </div>-->
                                            
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group subject">
                            <label><%=GetGlobalResourceObject("CommonControls", "F034_Titles")%>  :</label>
                            <input type="text" name="txtsubject" class="form-control txtsubject" required>
                        </div>
                        <div class="form-group">
                    
               <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F034_Message")%> *</label>
               
                <textarea id="txtComments" maxlength="1000" data-bind="textInput: Comments" class="Description validationElement" rows="8"></textarea>
                  </div>                    
                        <div class="form-group">
                            <button type="submit" name="submit" class="btn btn-primary btn-lg" required="required" data-bind="click: $root.Save">Submit Message</button>
                        </div>
                    </div>
                </form> 
            </div><!--/.row-->
        </div><!--/.container-->
 
  

</asp:Content>
