<%@ Page Title="<%$ Resources:CommonControls, F018 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="TrainingSearch.aspx.cs" Inherits="LMIS.Portal.LabourExchange.TrainingSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/TrainingSearch.js" async="async"></script>

    <div class="row">
        <div class="col-lg-2"></div>
        <div class="col-lg-8 " style="padding: 30px 20px;">
            <div class="input-group">
                <input id="txtKeywords" type="text" class="form-control" data-bind="textInput: Keywords">
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button" data-bind="click: Reload">
                        <i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Search")%>
                    </button>
                </span>
            </div>
        </div>
        <div class="col-lg-2"></div>
    </div>

    <div class="col-lg-9 latest">
        <table id="grd" style="border-spacing: 0; width: 100%;">
            <thead>
                <tr>
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
                                <h3 class="title"><a href="#" target="_blank" data-bind="text: Title, attr: { href: $root.TrainingDetails(Id) }"></a></h3>
                                <p data-bind="text: lmis.globalString.toLocal(Description, true)"></p>
                                <p><a class="more-link" href="#" target="_blank" data-bind="attr: { href: $root.TrainingDetails(Id) }">
                                    <i class="fa fa-external-link"></i><%=GetGlobalResourceObject("CommonControls", "X_FindOutMore")%>
                                </a></p>
                            </div>
                        </div>
                    </td>
                    <td data-bind="text: PortalUserId"></td>
                    <td data-bind="text: Country"></td>
                    <td data-bind="text: City"></td>
                </tr>
            </tbody>
        </table>
    </div>

    <div class="col-lg-3">
        <h4><%=GetGlobalResourceObject("CommonControls", "X_Filters")%></h4>
        <hr />
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Organization")%></label>
                <select class="form-control" data-bind="options: OrgOptions, value: Org, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
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

</asp:Content>