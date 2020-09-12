﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="TrainingInformation.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.TrainingInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row form-divider col-sm-12">
        <div class="col-lg-4">
            <hr>
        </div>
        <div class="col-lg-4 form-divider-title">
            <%=GetGlobalResourceObject("CommonControls", "F009")%>
        </div>
        <div class="col-lg-4">
            <hr>
        </div>
    </div>
    <section id="user-registration22">
        <div class="container white-bg padding20">
            <div class="col-md-12">
                <div class="panel with-nav-tabs panel-default">
                    <div class="panel-heading">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab1default" data-toggle="tab">My Account</a></li>
                            <li><a href="#tab2default" data-toggle="tab"><%=GetGlobalResourceObject("CommonControls", "F009_CV")%></a></li>
                            <li><a href="#tab3default" data-toggle="tab"><%=GetGlobalResourceObject("CommonControls", "F009_JobSearch")%></a></li>
                            <!--<li><a href="#tab4default" data-toggle="tab">Recommendations</a></li>-->
                        </ul>
                    </div>
                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="tab1default">
                                <div class="col-lg-12">
                                    <div class="progress-wrap">
                                        <h3>Total Points: 100
                                            <img src="../images/star-on.png" /></h3>
                                        <div class="progress">
                                            <div class="progress-bar  color1" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 85%">
                                                <span class="bar-width">85% Complete</span>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <!--Done-->
                                    <div class="stepwizard">
                                        
                                            <div class="stepwizard-row setup-panel">
                                                <div class="stepwizard-step">
                                                    <a href="PersonalInformation.aspx?mode=e" type="button" class="btn btn-default btn-circle" data-bind="click: PersonalInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>1 </a>
                                                    <p><%=GetGlobalResourceObject("CommonControls", "F009_PersonalInfo")%></p>
                                                </div>
                                                <div class="stepwizard-step">
                                                    <a href="EducationalInformation.aspx#anchor" type="button" class="btn btn-default btn-circle" data-bind="click: EducationInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%></span>2</a>
                                                    <p><%=GetGlobalResourceObject("CommonControls", "F009_Education")%></p>
                                                </div>
                                                <div class="stepwizard-step">
                                                    <a href="ExperienceInformation.aspx#anchor" type="button" class="btn btn-default btn-circle" data-bind="click: ExperienceInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%></span>3</a>
                                                    <p><%=GetGlobalResourceObject("CommonControls", "F009_Experience")%></p>
                                                </div>
                                                <div class="stepwizard-step">
                                                    <a href="SkillsInformation.aspx#anchor" type="button" class="btn btn-default btn-circle" data-bind="click: SkillsInfo"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>4</a>
                                                    <p><%=GetGlobalResourceObject("CommonControls", "F009_Skills")%></p>
                                                </div>
                                                <div class="stepwizard-step">
                                                    <a href="#step-5" type="button" class=" btn btn-primary btn-circle"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>5</a>
                                                    <p><%=GetGlobalResourceObject("CommonControls", "F009_TrainingandCertifications")%></p>
                                                </div>
                                            </div>
                                        </div>
                                        <!--Done-->
                                        <form role="form">
                                            <div class="row setup-content" id="step-5">
                                                <div class="col-xs-12">
                                                    <!--<div class="col-md-12">
                                                    <h3> Step 5</h3>
                                                    <img src="../images/tab02/reg_05.png" class="img-responsive" />
                                                    <button class="btn btn-success btn-lg pull-right" type="submit">Finish!</button>
                                                </div>-->

                                                    <div class="col-md-12">
                                                        <h3><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> 5</h3>

                                                        <div class="row form-divider ">
                                                            <div class="col-lg-4">
                                                                <hr>
                                                            </div>
                                                            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("MessagesResource", "X_Training")%>  </div>
                                                            <div class="col-lg-4">
                                                                <hr>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-10 col-sm-offset-1 text-center">
                                                            <div class="form-group">
                                                                <a id="postNew" href="AddNewTraining?m=a&id=0#anchor" class=" btn btn-primary  btn-lg">
                                                                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F009_AddTraining")%>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-10 col-sm-offset-1 text-center">
                                                            <div class="table-responsive">
                                                                <table id="grdTrainingInformation" class="display" style="border-spacing: 0; width: 100%;">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Training</th>
                                                                            <th>Provider</th>
                                                                            <th>Start Date</th>
                                                                            <th>End Date</th>
                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody data-bind="foreach: TrainingList">
                                                                        <tr data-bind="attr: { id: RowId }">
                                                                            <td data-bind="text: TrainingName"></td>
                                                                            <td data-bind="text: TrainingProviderName"></td>
                                                                            <td data-bind="text: TrainingStartDate"></td>
                                                                            <td data-bind="text: TrainingEndDate"></td>
                                                                            <td>
                                                                                <a href="#" title=" Edit " data-bind="click: $root.Edit"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Edit</a>
                                                                                <a href="#" title=" Delete " data-bind="click: $root.Delete"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Delete</a>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="row form-divider ">
                                                            <div class="col-lg-4">
                                                                <hr />
                                                            </div>
                                                            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "X_Certificate")%>  </div>
                                                            <div class="col-lg-4">
                                                                <hr />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-10 col-sm-offset-1 text-center">
                                                            <div class="form-group">
                                                                <a id="postNew2" href="AddNewCertificate?m=a&id=0#anchor" class=" btn btn-primary  btn-lg">
                                                                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F009_AddNewCertificate")%>
                                                                </a>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-10 col-sm-offset-1 text-center">
                                                            <div class="table-responsive">
                                                                <table id="grdCertificateInformation" class="display" style="border-spacing: 0; width: 100%;">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Certification Name</th>
                                                                            <th>Certification Issue Date</th>
                                                                            <th>Certification Valid Until</th>
                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody data-bind="foreach: CertificationList">
                                                                        <tr data-bind="attr: { id: RowId }">
                                                                            <td data-bind="text: CertificationName"></td>
                                                                            <td data-bind="text: CertificationIssueDate"></td>
                                                                            <td data-bind="text: CertificationValidUntil"></td>
                                                                            <td>
                                                                                <a href="#" title=" Edit " data-bind="click: $root.Editcrt"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Edit</a>
                                                                                <a href="#" title=" Delete " data-bind="click: $root.Deletecrt"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Delete</a>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>



                                                        <div class="col-sm-12">
                                                            <button class="btn btn-success btn-lg pull-right" type="submit">Finish!</button>
                                                        </div>


                                                    </div>

                                                </div>
                                            </div>
                                        </form>
        </div>
        </div>


                                <!-- <div class="tab-pane fade" id="tab4default">Default 4</div>-->
                            </div>
                        </div>
                    </div>
                </div>
                <div style="display: none;">
                    <div id="RequiredFieldsstep1">
                        <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                        <p><%=GetGlobalResourceObject("MessagesResource", "F009_RequiredFieldsstep2")%></p>
                    </div>
                </div>
            </div>
    </section>
    <script src="../IndividualRegistration/Scripts/TrainingInformation.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="Scripts/js.js"></script>

    <script>
        $(document).ready(function () {


            var navListItems = $('div.setup-panel div a'),
    allWells = $('.setup-content');
            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                        $item = $(this);

                if (!$item.hasClass('disabled')) {
                    navListItems.removeClass('btn-primary').addClass('btn-default');
                    $item.addClass('btn-primary');
                    allWells.hide();
                    $target.show();
                    $target.find('input:eq(0)').focus();
                }
            });
            $('#live-chat').click(function () { $('#live-chat-panel').show(300); });
            $('#live-chat-hide').click(function () { $('#live-chat-panel').hide(300); });
            $('#pie-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/pie-full.png');
                }
            });

            $('#bar-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/bar-chart.png');
                }
            });


            $('#line-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/line-full.png');
                }
            });


            $('#time-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/time-series.png');
                }
            });


            $('#pub-table').on({
                'click': function () {
                    $('#my-pub-graph').attr('src', '../images/tab08/table-graphs.png');
                }
            });

            $('#pub-graph').on({
                'click': function () {
                    $('#my-pub-graph').attr('src', '../images/tab08/graph-graphs.png');
                }
            });

            $('#pub-top10').on({
                'click': function () {
                    $('#my-pub-graph').attr('src', '../images/tab08/top10-graphs.png');
                }
            });

            $('div.setup-panel div a.btn-primary').trigger('click');

        });</script>
</asp:Content>
