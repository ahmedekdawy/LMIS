<%@ Page Title="<%$ Resources:CommonControls, F002 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="ContactPerson.aspx.cs" Inherits="LMIS.Portal.LabourExchange.ContactPerson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

                        <div class="tab-pane fade in active" id="tab2default">
                        
                        <div class="col-md-12">
               
                 <div class="row form-divider "> 
                        <div class="col-lg-4" > <hr> </div>
                        <div class="col-lg-4 form-divider-title " > Employee  Details </div>
                        <div class="col-lg-4" > <hr> </div>
                    </div> 
                    
                   <div class="col-sm-5 col-sm-offset-1">
                        <div class="form-group">
                            <label>Full Name  *</label>
                            <input type="text" name="name" class="form-control validationElement" required>
                        </div>
                        
                        <div class="form-group">
                            <label>Password</label>
                            <input type="password" class="form-control">
                        </div>
                        
                        
                        <div class="form-group">
                            <label> Job Title </label>
                            <input type="text" name="Job title" class="form-control" >
                        </div>
                        
                        
                         
                        
                      
                                              
                    </div>
                 <div class="col-sm-5">
                 
                 
             			<div class="form-group">
                            <label>Email</label>
                            <input type="text" class="form-control">
                        </div>
                        
                         <div class="form-group">
                            <label>Confirm Password</label>
                            <input type="password" class="form-control">
                        </div>
                        
                        <div class="form-group">
                            <label>Department</label>
                            <input type="text" class="form-control" placeholder="Ex.IT" />
                        </div>
                       
                        
                    </div>
                    
                    
                     <div class="row form-divider "> 
                        <div class="col-lg-4" > <hr/> </div>
                        <div class="col-lg-4 form-divider-title " > Contacts Information </div>
                        <div class="col-lg-4" > <hr/> </div>
                    </div> 
                    
                    <div class="col-sm-5 col-sm-offset-1">
                           <div class="form-group">
                            <label>Telephone No.</label>
                            <input type="text" class="form-control">
                        </div>
                        
                          <div class="form-group">
                            <label> Fax </label>
                            <input type="text" class="form-control">
                        </div>
                        
                                              
                    </div>
                    <div class="col-sm-5">
                       <div class="form-group">
                            <label>Mobile No. </label>
                            <input type="text" name="name" class="form-control" >
                        </div>
                        
                      
                    </div>
                    
                    
                    
                    
                    
                     
                     <div class="row form-divider col-sm-12 "> 
                        <div class="col-lg-4" > <hr> </div>
                        <div class="col-lg-4 form-divider-title " >Authorization letter </div>
                        <div class="col-lg-4" > <hr> </div>
                    </div>
                    
                       <div class="col-sm-5 col-sm-offset-1">
                         <div class="form-group ">
                         					
                                            <label>Authorization Letter </label>
                                             
                                           <div class="row text-center">
                                           <button class="btn btn-primary" type="button"><i class="fa fa-download"></i> Download </button>
                        </div> 
                                        </div>   
                                        
                    </div>
                    
                    <div class="col-sm-5 ">
                        <div class="form-group ">
                         					
                        <label> Authorization Letter  </label>
                        <div class="input-group">
                        <input type="text" class="form-control">
                        <span class="input-group-btn">
                        <button class="btn btn-default" type="button"><i class="fa fa-search"></i> Select </button>
                        
                        </span>
                       </div>
                       <p class="form-description">Upload the downloded template</p>
                       <div class="row text-center">
                       <input value="Upload" class="btn  btn-default btn-primary" type="button" />
                       </div> 
                              </div>
                                        
                    </div>
                    
                    
                    <div class="col-sm-12">
                    <button class="btn btn-success nextBtn btn-lg pull-right" type="button" > Save </button>
                    </div>
                    
              
            </div>
                        
                        </div>

</asp:Content>