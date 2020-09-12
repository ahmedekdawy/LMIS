<%@ Page Title="<%$ Resources:CommonControls,Menu_SiteMap %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="LMIS.Portal.FrontEnd.SiteMap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
            <div class="">
                <div class="faq container">
                    <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2>  <%=GetGlobalResourceObject("CommonControls", "Menu_Home")%> </h2>
                          <div class="questions"> 
                              <p>
                                <a class="site-map-link" href="../home" > 	<i class="fa fa-angle-double-right"></i> <%=GetGlobalResourceObject("CommonControls", "Menu_Home")%> </a>
                                <a class="site-map-link" href="../Faq" > 		<i class="fa fa-angle-double-right"></i> <%=GetGlobalResourceObject("CommonControls", "F057")%></a>
                                <a class="site-map-link" href="../Calendar" > <i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Calendar")%> </a>
                                <a class="site-map-link" href="Concepts-and-Definitions.html" > <i class="fa fa-angle-double-right"></i> Concepts and Definitions </a>
                                <a class="site-map-link" href="../HelpFullLinks" > <i class="fa fa-angle-double-right"></i> <%=GetGlobalResourceObject("CommonControls", "Menu_HelpfulLinks")%> </a>
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                     
                     <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2> <%=GetGlobalResourceObject("CommonControls", "X_LabourExchange")%> </h2>
                          <div class="questions"> 
                              <p>
                                  
                               <a href="../LabourExchange/JobSearch"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "F018")%></a>
                               <a href="../LabourExchange/TrainingSearch"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "F020")%></a>

                      
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                   
                     
                     <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2> <%=GetGlobalResourceObject("CommonControls", "Menu_LabourData")%>  </h2>
                          <div class="questions"> 
                              <p>
                                <a class="site-map-link" href="../HarmonizedData" > 
                                	<i class="fa fa-angle-double-right"></i> <%=GetGlobalResourceObject("CommonControls", "Menu_LabourData")%> </a>
                                
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                     
                     <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2><%=GetGlobalResourceObject("CommonControls", "Menu_LabourMarket")%>  </h2>
                          <div class="questions"> 
                              <p>
                
                               <a href="../LabourMarket/AverageWages"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_AverageWages")%> </a>
                               <a href="tab5/labour-unions-and-work-offices.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_LabourUnionsworkoffices")%></a>
                               <a href="tab5/list-of-recruitment-agencies.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_RecruitmentAgencies")%></a>
                               <a href="tab5/career-guidance.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Careerguidancejobseekers")%></a>
                               <a href="tab5/career-guidance.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Careerguidanceemployers")%></a>
                       
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                     
                     <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2><%=GetGlobalResourceObject("CommonControls", "Menu_InformalSector")%> </h2>
                          <div class="questions"> 
                              <p>
                            
                               <a href="../FrontEnd/ConceptOfNonFormalTraining"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "F052")%></a>
                               <a href="../LabourMarket/PastAchievements"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Pastachievements")%></a>
                               <a href="../LabourExchange/Opportunities"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Opportunities")%></a>
                               <a href="../LabourMarket/NewsFeed"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Newsfeed")%></a>
                               <a href="tab7/size-of-the-informal-sector.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Sizeinformalsector")%></a>
                               <a href="tab7/structure-of-the-informal-sector.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_StructureinformalSector")%></a>
                           
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                     <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2>Qualifications</h2>
                          <div class="questions"> 
                              <p>
                               <a href="tab9/qualifications.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_Qualifications")%>   </a>
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                    

   
                    <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2><%=GetGlobalResourceObject("CommonControls", "Menu_DataBank")%> </h2>
                          <div class="questions"> 
                              <p>
                                  <a href="tab9/data-bank.html"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_DataBank")%>   </a>
                       
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                     
                     
                     <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2><%=GetGlobalResourceObject("CommonControls", "Menu_ContactUs")%> </h2>
                          <div class="questions"> 
                              <p>
                                   <a href="../ContactUs"><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "F034_Title")%>   </a>
                        
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                       <div class="col-md-6 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq">
                          <h2><%=GetGlobalResourceObject("CommonControls", "Menu_Publication")%> </h2>
                          <div class="questions"> 
                              <p>
                         
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=3')" ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_InformalSectorStudies")%> </a>
                                 
                         
                            
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=4')"  ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_InformalSectorPublication")%> </a>
                                   
                            
                             
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=5')" ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_LMISAnnualReport")%> </a>
                                
                               
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=6')"  ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_SurveysInventory")%> </a>
                               
                           
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=7')"  ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_GeneralStudies")%> </a>
                               
                             
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=8')"  ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_PublishedMaterial")%> </a>
                                
                                
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=9')" ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_EmployersCareerGuidanceArticles")%> </a>
                              
                               
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=10')" ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_JobSeekersCareerGuidanceArticles")%> </a>
                             
                            
                                    <a href="#" onclick="return bm('Search/frmGlobalSearch.aspx?cid=LMIS&&lib=11')" ><i class="fa fa-angle-double-right"></i><%=GetGlobalResourceObject("CommonControls", "Menu_InformalSectorConceptsTraining")%> </a>
                              
                               
                              </p>
                          </div>
                        </div>
                     </div><!--/.col-md-6-->
                   

                     
                     
                     
                   
                    
                </div><!--/.services-->
            </div><!--/.row--> 

</asp:Content>
