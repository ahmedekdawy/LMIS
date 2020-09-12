<%@ Page Title="<%$ Resources:CommonControls,F038_Profile %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LMIS.Portal.Individual.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper">
        <div class="container content profile row">

            <div class="col-md-3 md-margin-bottom-40 profile">
                <img class="img-responsive profile-img margin-bottom-20" data-bind="attr: { src: PhotoPath }" alt="">

                <h2 class="text-center"><span data-bind="text: FullName"></span></h2>
                <ul class="list-group sidebar-nav-v1 margin-bottom-40 orginization" id="sidebar-nav-1">
                    <li class="list-group-item active" id="liProfile">
                        <a href="#" data-bind="click: function () { $('#divProfile').show(); $('#divAppliedJobs').hide(); $('.list-group-item').removeClass('active'); $('#liProfile').addClass('active'); }"><i class="fa fa-bar-chart-o"></i><%=GetGlobalResourceObject("CommonControls", "F038_Profile")%></a>
                    </li>
                    <li class="list-group-item" id="liAppliedJobs">
                        <a href="#" data-bind="click: function () { $('#divProfile').hide(); $('#divAppliedJobs').show(); $('.list-group-item').removeClass('active'); $('#liAppliedJobs').addClass('active'); }"><i class="fa fa-bar-chart-o"></i><%=GetGlobalResourceObject("CommonControls", "F038_AppliedJobsandTraining")%></a>
                    </li>
                    <li class="list-group-item">
                        <a id="aPersonalInfo" href="#" data-bind="click: function () { $root.dlgEditPersonalInfo.open($data); $('#ifamePersonInfo').attr('src', 'EditPersonalInfo.aspx?m=e'); }"><i class="fa fa-bar-chart-o"></i><%=GetGlobalResourceObject("CommonControls", "F038_EditProfile")%></a>
                    </li>
                    <li class="list-group-item">
                        <a id="aEducationalInfo" data-bind="click: function () { $root.dlgEditEducationalInfo.open($data); $('#ifameEducationInfo').attr('src', 'EditEducationalInfo.aspx?m=v&id=0'); }" href="#"><i class="fa fa-user"></i><%=GetGlobalResourceObject("CommonControls", "F038_AddEducation")%></a>
                    </li>
                    <li class="list-group-item">
                        <a id="aExperienceInfo" data-bind="click: function () { $root.dlgEditExperienceInfo.open($data); $('#ifameExperienceInfo').attr('src', 'EditExperienceInfo.aspx?m=v&id=0'); }" href="#"><i class="fa fa-group"></i><%=GetGlobalResourceObject("CommonControls", "F038_AddExperience")%></a>
                    </li>
                    <li class="list-group-item">
                        <a id="aSkillInfo" data-bind="click: function () { $root.dlgEditSkillInfo.open($data); $('#ifameSkillInfo').attr('src', 'EditSkillInfo.aspx?m=v&id=0'); }" href="#"><i class="fa fa-list"></i><%=GetGlobalResourceObject("CommonControls", "F038_AddSkill")%></a>
                    </li>
                    <li class="list-group-item">
                        <a id="aTrainingInfo" data-bind="click: function () { $root.dlgEditTrainingInfo.open($data); $('#ifameTrainingInfo').attr('src', 'EditTrainingInfo.aspx?m=v&id=0'); }" href="#"><i class="fa fa-comments"></i><%=GetGlobalResourceObject("CommonControls", "F015_AddTrainingList")%></a>
                    </li>
                    <li class="list-group-item">
                        <a id="aCertificateInfo" data-bind="click: function () { $root.dlgEditCertificateInfo.open($data); $('#ifameCertificateInfo').attr('src', 'EditCertificateInfo.aspx?m=v&id=0'); }" href="#"><i class="fa fa-certificate"></i><%=GetGlobalResourceObject("CommonControls", "F038_AddCertificate")%></a>
                    </li>
                    <%-- <li class="list-group-item">
                        <a href="page_profile_settings.html"><i class="fa fa-cog"></i>Settings</a>
                    </li>--%>
                </ul>

                <%--    <div data-bind="foreach: Jobs" class="alert-blocks alert-blocks-pending alert-dismissable">
                    <img class="rounded-x mCS_img_loaded" src="assets/img/testimonials/img3.jpg" alt="">
                    <div class="overflow-h">
                        <strong data-bind="text: Title"><small class="pull-right" data-bind="    text: lmis.format.approvalStatus(ViewStatus)"></small></strong>
                        <p></p>
                    </div></div>--%>
                <div class="row form-divider addSkill" data-bind="visible: (Skills().length > 0)">
                    <div id="lblSkills" class="form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_Skills")%></div>
                </div>

                <div data-bind="foreach: Skills">

                    <h3 class="heading-xs"><span data-bind="text: Skill.desc"></span><span class="pull-right" data-bind="    text: SkillLevel.desc"></span></h3>
                    <div class="progress progress-u progress-xxs">
                        <div data-bind="style: { width: SkillLevelPercentage + '%' }, attr: { ariaValuenow: SkillLevelPercentage }" aria-valuemax="100" aria-valuemin="0" role="progressbar" class="progress-bar progress-bar-u">
                        </div>
                    </div>
                </div>
                <hr>
            </div>
            <div class="col-md-9 profile">
                <div class="profile-body" id="divProfile">
                    <div class="col-md-12">
                        <div id="divPersonalInfo">

                            <div class="row form-divider ">
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                                <div class="col-lg-4 form-divider-title "><a data-toggle="collapse" data-target="#PersonalColapse"><%=GetGlobalResourceObject("CommonControls", "F038_PersonalData")%> <span class="fa fa-bars"></span> </a> </div>
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                            </div>
                            <div id="PersonalColapse" >
                            <div class="col-sm-5 col-sm-offset-1">
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "X_Email")%>: </label>
                                    <span  style="color:green" data-bind="text: Email"></span>
                                </div>
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "F009_MobileNo")%>:  </label>
                                    <span  style="color:green" data-bind="text: MobileNo"></span>
                                </div>
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "F009_DateofBirth")%>: </label>
                                    <span  style="color:green" data-bind="text: DateOfBirth"></span>
                                </div>
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Maritalstatus")%>: </label>
                                    <span  style="color:green" data-bind="text: Maritalstatus"></span>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "F009_TelephoneNo")%>: </label>
                                    <span  style="color:green" data-bind="text: TelephoneNo"></span>
                                </div>
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "X_Gender")%>: </label>
                                    <span  style="color:green" data-bind="text: Gender "></span>
                                </div>
                                <div class="form-group" data-bind="visible: GenderId() == '00200002'?true : false">
                                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Militarystatus")%>: </label>
                                    <span  style="color:green" data-bind="text: Militarystatus"></span>
                                </div>
                            </div>
                            <div class="col-sm-10 col-sm-offset-1">
                                <div class="form-group">
                                    <label><%=GetGlobalResourceObject("CommonControls", "F006_Address")%>: </label>
                                    <span  style="color:green" data-bind="text: AddressLocalized"></span>
                                </div>
                            </div>
                       </div>
                                
                                 </div>
                        <div id="divEducational">
                            <div class="row form-divider ">
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                                <div id="lblEducation" class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F038_Education")%></div>
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                            </div>

                            <div class="panel-body ">
                                <div class="addEducations " data-bind="visible: (Educations().length == 0)">
                                    <span><%=GetGlobalResourceObject("CommonControls", "F038_NodataEducationContent")%> <a class="orginization" href="#" data-bind="click: function () { $root.dlgEditEducationalInfo.open($data); }"><%=GetGlobalResourceObject("CommonControls", "F038_Clickhere")%></a></span>
                                </div>
                                <ul class="Educations" data-bind="foreach: Educations">
                                    <li>
                                        <span ><%=GetGlobalResourceObject("CommonControls", "F014_LevelofEducation")%>: </span>
                                        <span style="color:green" data-bind="text:LevelOfEducationName"></span>
                                         <span>  -  <%=GetGlobalResourceObject("CommonControls", "F009_InstitutionName")%>: </span>
                                        <span style="color:green" data-bind="text:EducationName"></span>
                                         <span>  -  <%=GetGlobalResourceObject("CommonControls", "F009_UniversityFacultyName")%>: </span>
                                        <span style="color:green" data-bind="text:FacultyID"></span>
                                         <span>  -  <%=GetGlobalResourceObject("CommonControls", "F009_UniversityGraduationyear")%>: </span>
                                        <span style="color:green" data-bind="text: GraduationYear"></span>
                                          <span>  -  <%=GetGlobalResourceObject("CommonControls", "F009_UniversityGrade")%>: </span>
                                        <span style="color:green" data-bind="text: Grade"></span>
                                          <span>  -  <%=GetGlobalResourceObject("CommonControls", "F009_UniversityGradeGPA")%>: </span>
                                        <span style="color:green" data-bind="text: GradeGPA"></span>
                              
                                        
                                       <a class="orginization " data-bind="click: function () { $root.dlgEditEducationalInfo.open($data); $('#ifameEducationInfo').attr('src', 'EditEducationalInfo.aspx?m=e&id=' + $data.IndividualEducationlevelID); }" href="#"><i class="fa fa-edit"></i></a>
                                       <a class="orginization "  data-bind="click: function () { $root.EducationDelete($data.IndividualEducationlevelID, $index()); }"><i class="fa fa-trash-o"></i></a>         
                                                     
                                       <br/>
                                    </li>
                                </ul>
                            </div>

                        </div>
                        <div id="divExperienceHistory">
                            <div class="row form-divider ">
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                                <div id="lblExperience" class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F038_WorkExperience")%> </div>
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="addExperience " data-bind="visible: (Experiences().length == 0)">
                                    <span><%=GetGlobalResourceObject("CommonControls", "F038_NoDataExperienceContent")%> <a class="orginization" href="#" data-bind="click: function () { $root.dlgEditExperienceInfo.open($data); }"><%=GetGlobalResourceObject("CommonControls", "F038_Clickhere")%></a></span>
                                </div>
                               <ul class="timeline-v2 timeline-me col-sm-12 profile Experience" data-bind="foreach: Experiences" >
                                    <li>
                                          
                                        <span class="col-sm-4 profile"  data-bind="text: EmploymentJobTitle"></span>
                                        <span class="col-sm-4 profile"  data-bind="text: Name"></span>
                                        <span class="col-sm-4 profile"  data-bind="text: EmploymentJobTitle"></span>
                                        <span class="col-sm-4 profile"  data-bind="text: JobDescription"></span>
                                         <span class="col-sm-4 profile" data-bind="text: lmis.format.dateToString(EmploymentStartDate) + ' - ' + ((EmploymentEndDate != null) ? lmis.format.dateToString(EmploymentEndDate) : '<%=GetGlobalResourceObject("CommonControls", "F038_current")%>    Current')"></span>
                                       

                                             <a class="orginization " id="aExperienceEdit" data-bind="click: function () { $root.dlgEditExperienceInfo.open($data); $('#ifameExperienceInfo').attr('src', 'EditExperienceInfo.aspx?m=e&id=' + $data.IndividualExperienceID); }" href="#"><i class="fa fa-edit"></i></a>
                                             <a class="orginization " id="aExperienceDel" data-bind="click: function () { $root.ExperienceDelete($data.IndividualExperienceID, $index()); }"><i class="fa fa-trash-o"></i></a>  
                                     
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div id="divSkills" >

                            <div class="row form-divider ">
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                                <div class="col-lg-4 form-divider-title "><a data-toggle="collapse" data-target="#skillsColapse"><%=GetGlobalResourceObject("CommonControls", "F009_Skills")%>  <span class="fa fa-bars"></span></a> </div>
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                            </div>

                            <div id="skillsColapse" class="panel-body collapse">
                                <div class="" data-bind="visible: (Skills().length == 0)">
                                    <span><%=GetGlobalResourceObject("CommonControls", "F038_NoDataSkillsContent")%> <a class="orginization" href="#" data-bind="click: function () { $root.dlgEditSkillInfo.open($data); }"><%=GetGlobalResourceObject("CommonControls", "F038_Clickhere")%></a></span>
                                </div>

                                <div data-bind="foreach: Skills" class="Skill">

                                    <h3 class="heading-s "><span data-bind="text: (SkillType.desc != null) ? SkillType.desc + ' - ' : ''"></span><span data-bind="    text: Skill.desc"></span></h3>
                                    <h3 class="heading-xs"><span data-bind="text: Industry.desc"></span><span class="pull-right" data-bind="    text: ' - ' + YearsOf_Experience + ' year(s)'"></span><span class="pull-right" data-bind="    text: SkillLevel.desc"></span></h3>
                                    <div class="progress progress-u progress-xs">
                                        <div data-bind="style: { width: SkillLevelPercentage + '%' }, attr: { ariaValuenow: SkillLevelPercentage }" aria-valuemax="100" aria-valuemin="0" role="progressbar" class="progress-bar progress-bar-u">
                                        </div>
                                    </div>
                                        
                                </div>
                            </div>
                        </div>
                        <div id="divTraining">

                            <div class="row form-divider ">
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                                <div id="lblTraining" class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F038_Training")%> </div>
                                <div class="col-lg-4">
                                    <hr>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="addTranings " data-bind="visible: (Trainings().length == 0)">
                                    <span><%=GetGlobalResourceObject("CommonControls", "F038_NodataTrainingContent")%> <a class="orginization" href="#" data-bind="click: function () { $root.dlgEditTrainingInfo.open($data); }"><%=GetGlobalResourceObject("CommonControls", "F038_Clickhere")%></a></span>
                                </div>

                                <ul class="timeline-v2 timeline-me Tranings" data-bind="foreach: Trainings">
                                    <li>
                                        <div class="col-sm-10">
                                        <span data-bind="text: TrainingProviderName + ' - ' + TrainingName"></span>  <br/><span data-bind="    text: lmis.format.dateToString(TrainingStartDate) + ' - ' + lmis.format.dateToString(TrainingEndDate)"></span><br/>
                                        <br/>
                                            </div>
                                        <div class="col-sm-2"> 
                                             <a class="orginization" id="aTrainingEdit" data-bind="click: function () { $root.dlgEditTrainingInfo.open($data); $('#ifameTrainingInfo').attr('src', 'EditTrainingInfo.aspx?m=e&id=' + $data.IndividualTrainingID); }" href="#"><i class="fa fa-edit"></i></a>
                                             <a class="orginization" id="aTrainingDel" data-bind="click: function () { $root.TrainingeDelete($data.IndividualTrainingID, $index()); }"><i class="fa fa-trash-o"></i></a>  
                                        </div>
                                       
                                    </li>
                                </ul>
                            

                        </div>
                        <div class="row form-divider ">
                            <div class="col-lg-4">
                                <hr>
                            </div>
                            <div id="lblCertificates" class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F038_Certificate")%> </div>
                            <div class="col-lg-4">
                                <hr>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="CertificateAdd " data-bind="visible: !(Certificates().length > 0)">
                                <span><%=GetGlobalResourceObject("CommonControls", "F038_NoDataCertificateContent")%> <a class="orginization" href="#" data-bind="click: function () { $root.dlgEditCertificateInfo.open($data); }"><%=GetGlobalResourceObject("CommonControls", "F038_Clickhere")%></a></span>
                            </div>
                            <ul class="timeline-v2 timeline-me Certificate" data-bind="foreach: Certificates">
                                <li>
                                    <span class="col-sm-12 profile"  data-bind="text: CertificateName"></span>
                                     <span class="col-sm-5 profile"  data-bind="text: '<%=GetGlobalResourceObject("CommonControls", "X_CertificationIssueDate")%>: ' + lmis.format.dateToString(CertificateIssueDate) "></span>
                                     <span class="col-sm-5 profile"  data-bind="text: '<%=GetGlobalResourceObject("CommonControls", "X_CertificationValidUntil")%>: ' + lmis.format.dateToString(CertificateValidUntil)"></span>
                                
                                      <a class="orginization col-sm-1" id="aTrainingEdit" data-bind="click: function () { $root.dlgEditCertificateInfo.open($data); $('#ifameCertificateInfo').attr('src', 'EditCertificateInfo.aspx?m=e&id=' + $data.IndividualCertificationID); }" href="#"><i class="fa fa-edit"></i></a>
                                      <a class="orginization col-sm-1" id="aTrainingDel" data-bind="click: function () { $root.CertificateDelete($data.IndividualCertificationID, $index()); }"><i class="fa fa-trash-o"></i></a>  
                                   

                                </li>
                            </ul>

                        </div>
                        <%--   <div class="col-sm-12 text-center">
                            <button class="btn btn-success btn-lg " type="submit"><i class="fa fa-download"></i>Save as PDF </button>
                        </div>--%>
                    </div>
                </div>
                    </div>
                <div class="profile-body" id="divAppliedJobs" style="display: none;">

                    <div class="row form-divider ">
                        <div class="col-lg-4">
                            <hr>
                        </div>
                        <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F038_AppliedJobs")%></div>
                        <div class="col-lg-4">
                            <hr>
                        </div>
                    </div>
                    
                    <div class="panel-body">
                        <div data-bind="visible: (Jobs().length == 0)">
                            <span><%=GetGlobalResourceObject("CommonControls", "F038_Nojobsappliedyet")%></span>
                        </div>
                        
                        <div data-bind="foreach: Jobs" class="profile-event">
                            <%--  <div class="date-formats">                               
                                <span data-bind="text: lmis.format.dateToString(ApplyDate).substr(0, 2)"></span>
                                <small data-bind="text: lmis.format.dateToString(ApplyDate).substr(3, 10)"></small>
                            </div>--%>
                            <div class="overflow-h">
                                <h3 class="heading-xs" data-bind="text: Title"></h3>
                                <a href="#" data-bind="text: OrganizationName"></a>
                                <ul class="list-unstyled">
                                    <li><span class="color-green">Status:</span><span data-bind="text: lmis.format.approvalStatus(ViewStatus)"></span></li>
                                    <li><span class="color-green">Applied on:</span> <span data-bind="text: lmis.format.dateToString(ApplyDate)"></span></li>
                                    <li><span class="color-green">Job Expired on:</span> <span data-bind="text: lmis.format.dateToString(ExpiryDate)"></span></li>
                                    <li><span class="color-green">Description:</span>
                                        <span data-bind="text: (JobDescription != null) ? JobDescription.substr(0, 150) + '...' : ''"></span>
                                        <a class="btn-u btn-u-sm pull-right" data-bind="click: function () { window.open('../LabourExchange/JobDetails.aspx?m=v&k=' + JobOfferID, Title); }" href="#">View More</a></li>
                                </ul>

                            </div>
                        </div>
                    </div>

                    <div class="row form-divider ">
                        <div class="col-lg-4">
                            <hr>
                        </div>
                        <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F038_AppliedTrainings")%></div>
                        <div class="col-lg-4">
                            <hr>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div data-bind="visible: (AppliedTrainings().length == 0)">
                            <span><%=GetGlobalResourceObject("CommonControls", "F038_Notrainingsappliedyet")%></span>
                        </div>
                        <div data-bind="foreach: AppliedTrainings" class="profile-event">
                            <%--  <div class="date-formats">                               
                                <span data-bind="text: lmis.format.dateToString(ApplyDate).substr(0, 2)"></span>
                                <small data-bind="text: lmis.format.dateToString(ApplyDate).substr(3, 10)"></small>
                            </div>--%>
                            <div class="overflow-h">
                                <h3 class="heading-xs" data-bind="text: CourseName"></h3>
                                <a href="#" data-bind="text: OrganizationName"></a>
                                <ul class="list-unstyled">
                                    <li><span class="color-green">Status:</span><span data-bind="text: lmis.format.approvalStatus(ViewStatus)"></span></li>
                                    <li><span class="color-green">Applied on:</span> <span data-bind="text: lmis.format.dateToString(ApplyDate)"></span></li>
                                    <li><span class="color-green">Job Expired on:</span> <span data-bind="text: lmis.format.dateToString(ExpiryDate)"></span></li>
                                </ul>
                                <p data-bind="text: (TrainingDescription != null) ? TrainingDescription.substr(0, 150) + '...' : ''"></p>
                                <a class="btn-u btn-u-sm" data-bind="click: function () { window.open('../LabourExchange/TrainingDetails?m=v&k=' + TrainingOfferID, CourseName); }" href="#">View More</a>
                            </div>
                        </div>
                    </div>
                </div>
            
 
            
            </div>
            <div id="dlgEditPersonalInfo">
                <form >
                    <fieldset>
                        <div class="form-group">
                            <iframe id="ifamePersonInfo" frameborder="0" src="EditPersonalInfo.aspx" style="overflow: hidden; height: 850px; width: 100%;" height="850"></iframe>
                        </div>
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position: absolute; top: -1000px">
                    </fieldset>
                </form>
            </div>
            <div id="dlgEditEducationalInfo">
                <form>
                    <fieldset>
                        <div class="form-group">
                            <iframe id="ifameEducationInfo" frameborder="0" src="EditEducationalInfo.aspx?id=0" scrolling="no" style="overflow: hidden; height: 580px; width: 100%;"></iframe>
                        </div>
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position: absolute; top: -1000px">
                    </fieldset>
                </form>
            </div>
            <div id="dlgEditExperienceInfo">
                <form>
                    <fieldset>
                        <div class="form-group">
                            <iframe id="ifameExperienceInfo" frameborder="0" src="EditExperienceInfo.aspx?id=0" scrolling="no" style="overflow: hidden; height: 460; width: 100%;"
                                height="460" width="100%"></iframe>
                        </div>
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position: absolute; top: -1000px">
                    </fieldset>
                </form>
            </div>
            <div id="dlgEditSkillInfo" >
                <form>
                    <fieldset>
                        <div class="form-group">
                            <iframe id="ifameSkillInfo" frameborder="0" src="EditSkillInfo.aspx?id=0" style="position: absolute; overflow: hidden;height:100%"  height="100%" width="100%"></iframe>
                        </div>
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position: absolute; top: -1000px">
                    </fieldset>
                </form>
            </div>
            <div id="dlgEditTrainingInfo">
                <form>
                    <fieldset>
                        <div class="form-group">
                            <iframe id="ifameTrainingInfo" frameborder="0" src="EditTrainingInfo.aspx?id=0" scrolling="no" style="overflow: hidden; width: 100%;" height="460" width="100%"></iframe>
                        </div>
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position: absolute; top: -1000px">
                    </fieldset>
                </form>
            </div>
            <div id="dlgEditCertificateInfo">
                <form>
                    <fieldset>
                        <div class="form-group">
                            <iframe id="ifameCertificateInfo" frameborder="0" src="EditCertificateInfo.aspx?id=0" scrolling="no" style="overflow: hidden; height: 400; width: 100%;"
                                height="400" width="100%"></iframe>
                        </div>
                        <!-- Allow form submission with keyboard without duplicating the dialog button -->
                        <input type="submit" tabindex="-1" style="position: absolute; top: -1000px">
                    </fieldset>
                </form>
            </div>
        </div>
        <div style="display: none">
            <span id="lblFirstName"><%=GetGlobalResourceObject("CommonControls", "F009_FirstName")%></span>
            <span id="lblLastName"><%=GetGlobalResourceObject("CommonControls", "F009_LastName")%></span>
            <span id="lblAddress"><%=GetGlobalResourceObject("CommonControls", "F009_Address")%></span>
        </div>
    </div>
    
   
    <script async="async" src="../Scripts/Extensions/ko.trilingualtext.js"></script>
    <script async="async" src="../Scripts/Extensions/ko.date.js"></script>
    <script async="async" src="../Scripts/Extensions/lmis.js"></script>
    <script async="async" src="../Individual/Scripts/profile-vm.js"></script>
    <script src="../Scripts/knockout.validation.js"></script>
    <script src="../Scripts/Extensions/strength.js"></script>
    <link href="../css/shortcode_timeline2.css" rel="stylesheet" />
    <link href="../css/profile.css" rel="stylesheet" />
    <link href="../css/app.css" rel="stylesheet" />
    <link href="../css/page_job.css" rel="stylesheet" />
    <script src="../Scripts/Extensions/config.js"></script>
     <script type="text/javascript">
         
        
         </script>
</asp:Content>
