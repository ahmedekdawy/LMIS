<%@ Page Title="<%$ Resources:CommonControls, X_LabourExchange %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="LabourExchange.aspx.cs" Inherits="LMIS.Portal.LabourExchange.LabourExchange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center">
        <h2>Labour Exchange </h2>
        <p class="lead">types of users and function which you can use as a registered user for LMIS</p>
    </div>

    <div class="pricing-area text-center">
        <div class="">

            <div class="col-sm-4 plan price-one wow fadeInDown">
                <ul>
                    <li class="heading-one">
                        <h1>Individual</h1>
                        <span>Registration</span>
                    </li>
                    <li>Create CV</li>
                    <li>preview CV</li>
                    <li>Seeking for Jobs</li>
                    <li>Enhance your skills</li>
                    <li>Get Recommendations</li>
                    <li class="plan-action">
                        <a href="../Individual/SignUp" class="btn btn-primary">Sign up</a>
                    </li>
                </ul>
            </div>

            <div class="col-sm-4 plan price-two wow fadeInDown">
                <ul>
                    <li class="heading-two">
                        <h1>Organizaion</h1>
                        <span>Registration</span>
                    </li>
                     <li ><a href="../LabourExchange/cvsearch"><%=GetGlobalResourceObject("CommonControls", "F003")%></a></li>
                    <li><a href="../LabourExchange/JobList"><%=GetGlobalResourceObject("CommonControls", "F004")%></a></li>
                    <li><a href="../LabourExchange/TrainingList"><%=GetGlobalResourceObject("CommonControls", "F015")%></a></li>
                    <li><a href="../LabourExchange/OpportunityList"><%=GetGlobalResourceObject("CommonControls", "F005")%></a></li>
                    <li><a href="../LabourExchange/EventList"><%=GetGlobalResourceObject("CommonControls", "F006")%></a></li>
                    <li><a href="../LabourExchange/ContactPerson"><%=GetGlobalResourceObject("CommonControls", "F002")%></a></li>
                    <li class="plan-action"><a href="../Organization/SignUp?as=org" class="btn btn-primary"><%=GetGlobalResourceObject("CommonControls", "X_SignUp")%></a>
                    </li>
                </ul>
            </div>
                 <div class="col-sm-4 plan price-two wow fadeInDown">
                <ul>
                    <li class="heading-three">
                        <h1>Self-employed</h1>
                        <span>Registration</span>
                    </li>
                     <li ><a href="../LabourExchange/cvsearch"><%=GetGlobalResourceObject("CommonControls", "F003")%></a></li>
                    <li><a href="../LabourExchange/JobList"><%=GetGlobalResourceObject("CommonControls", "F004")%></a></li>
                    <li><a href="../LabourExchange/TrainingList"><%=GetGlobalResourceObject("CommonControls", "F015")%></a></li>
                    <li><a href="../LabourExchange/OpportunityList"><%=GetGlobalResourceObject("CommonControls", "F005")%></a></li>
                    <li><a href="../LabourExchange/EventList"><%=GetGlobalResourceObject("CommonControls", "F006")%></a></li>
                    <li><a href="../LabourExchange/ContactPerson"><%=GetGlobalResourceObject("CommonControls", "F002")%></a></li>
                    <li class="plan-action"><a href="../Organization/SignUp?as=org" class="btn btn-primary"><%=GetGlobalResourceObject("CommonControls", "X_SignUp")%></a>
                    </li>
                </ul>
            </div>

        </div>
    </div>

</asp:Content>
