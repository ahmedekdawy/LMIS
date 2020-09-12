<%@ Page Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="CVSearch.aspx.cs" Inherits="LMIS.Portal.LabourExchange.CVSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link href="../css/star-rating.css" rel="stylesheet" />
    <script src="../js/star-rating.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Scripts/Extensions/searchablecombo.js" async="async"></script>
    <script src="../LabourExchange/Scripts/CVSearch.js" async="async"></script>

    <div class="row" data-bind="visible: JobOfferOptions().length > 0">
        <div class="col-lg-2"></div>
        <div class="col-lg-8" style="padding: 30px 20px;">
            <div style="width: 100%">
                <%=GetGlobalResourceObject("CommonControls", "F003_JobOffer")%>
            </div>
            <div class="input-group" style="width: 100%">
                <select class="form-control searchablecombo" data-bind="searchableCombo: true, options: JobOfferOptions, value: JobOffer, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select><p></p>
            </div>
            <div class="input-group">
                <span class="input-group-btn center">
                    <button class="btn btn-default" type="button" data-bind="click: Reload">
                        <i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Search")%>
                    </button>
                </span>
            </div>
        </div>
        <div class="col-lg-2"></div>
    </div>

    <div class="col-lg-9 latest">
        <div data-bind="visible: VmList().length > 0"><%=GetGlobalResourceObject("MessagesResource", "F003_ColorCode")%></div>
        <table id="grd" style="border-spacing: 0; width: 100%;">
            <thead>
                <tr>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody data-bind="foreach: VmList">
                <tr>
                    <td>
                        <div class="item row">
                            <div class="desc col-md-10 col-sm-10 col-xs-12">
                                <h3><a href="#" target="_blank" data-bind="text: Label, attr: { href: $root.CandidateDetails(Id) }"></a></h3>
                                <span style="color: green; font-weight: bolder" data-bind="visible: $root.JobOffer() && !(($root.ExpFrom.jo && YOE < $root.ExpFrom.jo) || ($root.ExpTo.jo && YOE > $root.ExpTo.jo))"><%=GetGlobalResourceObject("CommonControls", "F003_YOE")%></span>
                                <span style="color: red; font-weight: bolder" data-bind="visible: $root.JobOffer() && (($root.ExpFrom.jo && YOE < $root.ExpFrom.jo) || ($root.ExpTo.jo && YOE > $root.ExpTo.jo))"><%=GetGlobalResourceObject("CommonControls", "F003_YOE")%></span>
                                <span data-bind="visible: $root.JobOffer()"> - </span>
                                <span style="color: green; font-weight: bolder" data-bind="visible: $root.JobOffer() && MedFit"><%=GetGlobalResourceObject("CommonControls", "F003_MedFit")%></span>
                                <span style="color: red; font-weight: bolder" data-bind="visible: $root.JobOffer() && !MedFit"><%=GetGlobalResourceObject("CommonControls", "F003_MedUnFit")%></span>
                                <span data-bind="visible: $root.JobOffer()"> - </span>
                                <span style="color: green; font-weight: bolder" data-bind="visible: $root.JobOffer() && EdlevelMatching"><%=GetGlobalResourceObject("CommonControls", "F003_Edlevel")%></span>
                                <span style="color: red; font-weight: bolder" data-bind="visible: $root.JobOffer() && !EdlevelMatching"><%=GetGlobalResourceObject("CommonControls", "F003_Edlevel")%></span>
                            </div>
                            <div data-bind="visible: SkillRating >= 0">
                                <input class="StarRating" data-bind="attr: { value: SkillRating }" readonly="readonly" type="number" data-size="xs" data-symbol="*" >
                            </div>
                        </div>
                    </td>
                    <td data-bind="text: Skills"></td>
                    <td data-bind="text: Gender.id"></td>
                    <td data-bind="text: Experience"></td>
                    <td data-bind="text: Country.id"></td>
                    <td data-bind="text: City.id"></td>
                    <td data-bind="text: Jobs.join()"></td>
                    <td data-bind="text: EdlevelMatching"></td>
                    <td data-bind="text: MedFit"></td>
                </tr>
            </tbody>
        </table>
    </div>

    <div class="col-lg-3">
        <h4><%=GetGlobalResourceObject("CommonControls", "X_Filters")%></h4>
        <hr />
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "F004_JobTitle")%></label>
            <select class="form-control searchablecombo" data-bind="searchableCombo: true, options: JobTitleOptions, value: JobTitle, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
            </select>
        </div>
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "X_Skills")%></label>
            <select class="form-control searchablecombo" data-bind="searchableCombo: true, options: SkillOptions, value: Skill, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
            </select>
        </div>
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "X_Gender")%></label>
            <select class="form-control" data-bind="options: GenderOptions, value: Gender, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
            </select>
        </div>
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "X_YOfExperience")%></label>
            <input type="text" id="experience" readonly style="border: 0; color: #f6931f; font-weight: bold;">
            <div id="slider"></div>
        </div>
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "X_Country")%></label>
            <select class="form-control" data-bind="options: CountryOptions, value: Country, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
            </select>
        </div>
        <div class="form-group">
            <label><%=GetGlobalResourceObject("CommonControls", "X_City")%></label>
            <select class="form-control" data-bind="options: CityOptions, value: City, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
            </select>
        </div>
    </div>
    
    <span id="SkillRating" style="visibility: hidden"><%=GetGlobalResourceObject("CommonControls", "F003_SkillRating")%></span>

</asp:Content>