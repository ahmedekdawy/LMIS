<%@ Page Title="<%$ Resources:CommonControls,F032_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="LMIS.Portal.FrontEnd.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
 
    <form runat="server" >
   
   <section id="main-slider" class="no-margin">

   <div  class="container no-padding">
        <div id="Slides" class="carousel slide col-sm-9  no-padding-left">
            <ol class="carousel-indicators" data-bind="foreach: newsList">
                <li data-target="#main-slider"  data-bind="attr: { 'data-slide-to': $index() }, css: { 'active': $index() == 0 }"  ></li>
            </ol>
            <div class="carousel-inner" data-bind="foreach: newsList">
               
             <div class="item"  data-bind="css: { ' active': $index() == 0 }, style: { 'background-image': 'url(\'' + NewsBannerPath() + '\')'  }"  >
                    <div >
                        <div class="row slide-margin">
                        
                        <div class="col-sm-8 hidden-xs animation animated-item-4">
                                <div class="slider-img">
                                    <!--<img src="images/slider/img1.png" class="img-responsive">-->
                                </div>
                            </div>
                        
                            <div class="col-sm-4">
                                <div class="carousel-content">
                                   <!-- <h1 class="animation animated-item-1">Lorem ipsum dolor sit amet consectetur adipisicing elit</h1>-->
                                  <a class="btn-slide animation animated-item-4"  data-bind="attr: { href: newshref }" target="_blank"> <h2 class="animation animated-item-1" data-bind="    html: NewsTitle">  </h2></a>  
									 <h1 class="animation animated-item-2"> <hr/> </h1> 
                                  
                                     <%--<h2 class="animation animated-item-3"  data-bind="text: NewsDate"></h2>--%>
                                   
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div><!--/.item-->
           
            </div><!--/.carousel-inner-->
            <a class="prev hidden-xs" href="#Slides" data-slide="prev">
            <i class="fa fa-chevron-left"></i>
        </a>
        <a class="next hidden-xs" href="#Slides" data-slide="next">
            <i class="fa fa-chevron-right"></i>
        </a>
        </div><!--/.carousel-->
        
        <div class="col-sm-3 no-padding-right"> 
        <div class="login-form">
        <h3> Welcome To LMIS </h3> 
                          <form id="loginForm" >
                     
                              <div id="loginErrorMsg" class="alert alert-error hide">Wrong username og password</div>
                              <div class=" forget-check">
                              <div class="row quick-link">
                              <div class="col-sm-1"><i class="fa fa-search"></i></div>
                              <div class="col-sm-10"><a href="../LabourExchange/JobSearch"><%=GetGlobalResourceObject("CommonControls", "F018")%></a></div>
                              </div>
                              <div class="row quick-link">
                              <div class="col-sm-1"><i class="fa fa-laptop"></i></div>
                              <div class="col-sm-10"><a href="../LabourExchange/TrainingSearch">Train Your Employees</a></div>
                              </div>
                              
                               <div class="row quick-link">
                              <div class="col-sm-1"><i class="fa fa-users"></i></div>
                              <div class="col-sm-10"><a href="../LabourExchange/LabourExchange">Hire New Employees </a></div>
                              </div>
                              <div class="row quick-link">
                              <div class="col-sm-1"><i class="fa fa-trophy"></i></div>
                              <div class="col-sm-10"><a href="../LabourExchange/TrainingSearch"> Seeking Training ? </a></div>
                              </div>
                              
                               <div class="row quick-link">
                              <div class="col-sm-1"><i class="fa fa-bar-chart-o"></i></div>
                              <div class="col-sm-10"><a href="../HarmonizedData"> Economic Indicators </a></div>
                              </div>
                      
                              <div class="row quick-link">
                              <!--  <div class="col-sm-1"><i class="fa fa-money"></i></div>
                              <div class="col-sm-10"><a href="../EmployerRegisteration/Index.aspx?cat=GOV&subCat=SLF"> Services for Self Employed</a></div>
                              </div>
                                <label>
                                      <input type="checkbox" name="remember" id="remember"> Remember login  
                                  </label>
                                  <a href="#" class="btn-block forget-password" >Forget  password</a>-->
                              </div>
                              <a href="../login" class="btn btn-success btn-login btn-lg btn-block btn-login"><%=GetGlobalResourceObject("CommonControls", "F007")%></a>
                             
                              
                          </form>
                       </div>
                       <div>
                       	<a href="../LabourExchange/LabourExchange" class="btn btn-default btn-block btn-register"><%=GetGlobalResourceObject("CommonControls", "F032_Register")%> <i class="fa fa-angle-right custom-arrow"></i> </a>
                        
                         </div></div>
        
        </div>
       
        <div class="container white-bg no-padding" >
         
        	<div class="col-sm-9 all-records ">
             <a href="../AvailableVacancies">
            <div class="record record-01">
            	<div class="record-top record-01-top AvailableVacancies">  </div> 
                <div class="record-triangle record-01-triangle"></div> 
                <div class="record-body"> <%=GetGlobalResourceObject("CommonControls", "F045")%></div>
            </div>
            </a>
            <a href="../JobsOffers">
            <div class="record record-02">
            	<div class="record-top record-02-top JobsOffers">  </div> 
                <div class="record-triangle record-02-triangle"></div> 
                <div class="record-body"> <%=GetGlobalResourceObject("CommonControls", "F046")%> </div>
            </div>
            </a>
            <div class="record record-03">
            	<div class="record-top record-03-top UnEployeedJobSeekers">  </div> 
                <div class="record-triangle record-03-triangle"></div> 
                <div class="record-body">  <%=GetGlobalResourceObject("CommonControls", "F032_AvailableJobSeekers")%></div>
            </div>
            
            <div class="record record-04">
            	<div class="record-top record-04-top JobsApplied">  </div> 
                <div class="record-triangle record-04-triangle"></div> 
                <div class="record-body">   <%=GetGlobalResourceObject("CommonControls", "F032_JobsApplied")%></div>
            </div>
            
            <div class="record record-05">
            	<div class="record-top record-05-top JobSeekers">  </div>
                <div class="record-triangle record-05-triangle"></div>
                <div class="record-body">  <%=GetGlobalResourceObject("CommonControls", "F032_JobSeekers")%></div>
            </div>
           
            
            </div>
        	<div class="col-md-3 col-sm-6 col-xs-12 no-padding-right " >
            <div class="calender">
            <h2 class="hidden-xs">LMIS Calendar</h2>
            <a href="../Calendar" target="_blank" class="btn btn-default  calender-btn "> Follow our event</a>
            </div>
            </div>
        </div>
    </section>
    
     <section id="welcome-info">
        <div class="container white-bg">
            
                <div class="col-sm-9">
                    <div class="media welcome-info wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        
                      <div class="media-body">
                          <asp:Literal id="literalMessage" Text="I love ASP!" runat="server" />
                       
                       </div>
                    </div>
                </div>
                <div class="col-sm-3 quick-graphs">
               <div class="btn-group btn-block">
            

 

</div>

          
                
                </div>
            
        </div><!--/.container-->    
    </section><!--/#welcome-info-->
    
    
    <section id="feature" >
        <div class="container white-bg">
           <div class="center wow fadeInDown">
           <div class="container big-title">
             <div class="col-sm-4"> <hr /> </div>
             <div class="col-sm-4"> <h2> LMIS Services </h2>   </div>    
             <div class="col-sm-4"> <hr/> </div>
           </div>
                <!--<p class="lead">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut <br> et dolore magna aliqua. Ut enim ad minim veniam</p>-->
            </div>

            <div class="row">
                <div class="features">
                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <img src="../images/LaborExchangeThumbnail.jpg" class="img-responsive" />
                            <h2><%=GetGlobalResourceObject("CommonControls", "X_LabourExchange")%></h2>
                            <ul class="float-left">
                              <li><a href="../LabourExchange/JobSearch?f1=01800001#anchor"> <%=GetGlobalResourceObject("CommonControls", "F018_fullTime")%>  </a></li>
                              <li><a href="../LabourExchange/JobSearch?f1=01800002#anchor">  <%=GetGlobalResourceObject("CommonControls", "F018_partTime")%>  </a></li>
                              <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                            </ul>
                            <br/>
                            <div class="show-more"> <a href="tab2/individual-registeration-final.html" onclick="return false;" title="Show More">+</a> </div>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <img src="../images/Data-Warehouse_th.jpg" class="img-responsive" />
                            <h2><%=GetGlobalResourceObject("CommonControls", "Menu_LabourData")%></h2>
                            <ul class="float-left">
                              <li><a href="../HarmonizedData"> <%=GetGlobalResourceObject("CommonControls", "Menu_LabourData")%>  </a></li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                                <li>&nbsp;&nbsp;</li>
                            </ul>
                              <br/>
                            <div class="show-more"> <a href="tab3/labour-supply-components.html" onclick="return false;" title="Show More">+</a> </div>
                        </div>
                    </div><!--/.col-md-4-->
                     
                
                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap" >
                            <img src="../images/Labor-Market_th.jpg" class="img-responsive" />
                            <h2><%=GetGlobalResourceObject("CommonControls", "Menu_LabourMarket")%></h2>
                            <ul class="float-left">
                                                  
                               <li><a href="../LabourMarket/AverageWages"  ><%=GetGlobalResourceObject("CommonControls", "F047")%> </a></li>
                               <li><a href="../workoffices" ><%=GetGlobalResourceObject("CommonControls", "B001")%></a></li>
                                <li><a href="../LaborUnions" ><%=GetGlobalResourceObject("CommonControls", "B002")%></a></li>
                               <li><a href="../RecruitmentAgencies" ><%=GetGlobalResourceObject("CommonControls", "F062")%></a></li>
                               <li ><a href="../DescriptiveJob"><%=GetGlobalResourceObject("CommonControls", "F055")%></a></li>
                               <li ><a href="../TrainingProviders"><%=GetGlobalResourceObject("CommonControls", "F060")%></a></li>
                               <li ><a href="../Employers"><%=GetGlobalResourceObject("CommonControls", "F061")%></a></li>
                            </ul>
                            <div class="show-more"> <a href="#" title="Show More">+</a> </div>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap" >
                            <img src="../images/InformalSectorThumbnail.jpg" class="img-responsive" />
                            <h2>Informal sector</h2>
                            <ul class="float-left">
                            <li><a href="#">  Size of the informal sector</a></li>
                            <li><a href="#">  Structure of the informal sector</a></li>
                            <li><a href="#">  Non-formal training   </a></li>
                            <!--<li><a href="#">  Bibliography on informal sector   </a></li>-->
                            </ul>
                            <div class="show-more"> <a href="#" title="Show More">+</a> </div>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <img src="../images/PublicationsThumbnail.jpg" class="img-responsive" />
                            <h2><%=GetGlobalResourceObject("CommonControls", "Menu_Publication")%></h2>
                            <ul class="float-left">
                            <li><a href="http://69.25.178.25:8062/Search/browse.aspx?cid=LMIS&&lib=pm">  <%=GetGlobalResourceObject("CommonControls", "Menu_PublishedMaterial")%> </a></li>
                            <li><a href="http://69.25.178.25:8062/Search/browse.aspx?cid=LMIS&&lib=gs">  <%=GetGlobalResourceObject("CommonControls", "Menu_GeneralStudies")%></a></li>
                            <li><a href="ttp://69.25.178.25:8062/Search/browse.aspx?cid=LMIS&&lib=si"> <%=GetGlobalResourceObject("CommonControls", "Menu_SurveysInventory")%> </a></li>
                            </ul>
                            <div class="show-more"> <a href="#" title="Show More">+</a> </div>
                        </div>
                    </div><!--/.col-md-4-->
           
                </div><!--/.services-->
            </div><!--/.row-->    
        </div><!--/.container-->
    </section><!--/#feature-->

    <section id="welcome-info">
     <div class="container white-bg">
           <div class="center wow fadeInDown"> 
           <div class="container big-title">
             <div class="col-sm-4"> <hr /> </div>
             <div class="col-sm-4"> <h2>Our Partners</h2>  </div>    
             <div class="col-sm-4"> <hr/> </div>
           </div>
            </div>
           <div class="container">
        
                    <ul id="flexiselDemo3" data-bind="foreach: PartnersList" class="nbs-flexisel-ul"  style="direction: ltr; float: left;">
                   <li class="nbs-flexisel-item"><a target="_blank" data-bind="attr: { href: '../Partners/Details?PartnerID=' + $data.PartnerID, title: OrganizationName }" >
                   <img   data-bind="attr: { src: '../Uploads/' + OrganizationLogoPath, alt: OrganizationName }" class="img-responsive"/>
                   </a></li>
                   </ul>
              
                   
                  
                  
        </div>   
     </div>
    </section>

    </form>
    <script src="../FrontEnd/Scripts/home.js"></script>
</asp:Content>
