<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCuyaLightBox.aspx.cs" Inherits="Cuyahoga.Modules.CuyaLightBox.Web.Admin.AdminCuyaLightBox" MaintainScrollPositionOnPostback="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" ><head runat="server"><title>CuyaLightBox Admin</title></head><body><form id="form1" runat="server">
        <div id="adminpanel" class="AdminPanel" >
        <h2><asp:Label ID="lblModuleSetting" runat="server" Text=""></asp:Label></h2>
        <asp:Label ID="lblMessages" runat="server" Text=""></asp:Label>
        
            <div id="lightboxinfo" class="Panel60">
                <fieldset>
                    <legend>LightBox</legend>
                    <asp:Label ID="LabelName" AssociatedControlID="TextBoxName" Text="Name" runat="server"></asp:Label>
                    <asp:TextBox ID="TextBoxName" ValidationGroup="lightbox" CausesValidation="true" runat="Server"></asp:TextBox><br/>
                    
                    <asp:Label ID="LabelDescription" AssociatedControlID="TextBoxDescription" Text="Description" runat="server"></asp:Label>
                    <asp:TextBox ID="TextBoxDescription" TextMode="MultiLine" Rows="4" runat="Server"></asp:TextBox><br/>
                    
                    <asp:Label ID="LabelActive" AssociatedControlID="CheckBoxActive" Text="Is Active" runat="server"></asp:Label>
                    <asp:CheckBox ID="CheckBoxActive" runat="server" />
                    <br/>
                    <asp:Button ID="btnLightBox" runat="server" Text="Create LightBox" CausesValidation="true" CssClass="SubmitButton" onclick="btnLightBox_Click" ValidationGroup="lightbox" />
                </fieldset>
                <hr />
                <asp:GridView ID="gvLightbox" runat="server" 
                                DataKeyNames="LightBoxId" 
                                AutoGenerateColumns="False" 
                                onselectedindexchanged="gvLightbox_SelectedIndexChanged" 
                                onrowediting="gvLightbox_RowEditing"
                                onrowcancelingedit="gvLightbox_RowCancelingEdit" 
                                onrowupdating="gvLightbox_RowUpdating" onrowdeleting="gvLightbox_RowDeleting" >
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="LightBox Name" />
                        <asp:BoundField DataField="Created" HeaderText="Date Created" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Is Active" />

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="False" CommandName="Select" Text="Make Current"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this LightBox item?');" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="lightboximageinfo" class="Panel60">
                <fieldset>
                    <legend>Lightbox Images</legend>
                    <asp:Label ID="LabelFilename" AssociatedControlID="ImageFileUpload" Text="Filename" runat="server"></asp:Label>
                    <asp:FileUpload ID="ImageFileUpload" runat="Server"></asp:FileUpload><hr />
                    <asp:Label ID="LabelTitle" AssociatedControlID="TextBoxTitle" Text="Title" runat="server"></asp:Label>
                    <asp:TextBox ID="TextBoxTitle" runat="Server"></asp:TextBox><br/>
                    <asp:Label ID="LabelAltText" AssociatedControlID="TextBoxAltText" Text="AltText" runat="server"></asp:Label>
                    <asp:TextBox ID="TextBoxAltText" runat="Server"></asp:TextBox><br/>
                    <asp:Label ID="lblImageActive" AssociatedControlID="cbxImageActive" Text="Is Active" runat="server"></asp:Label>
                    <asp:CheckBox ID="cbxImageActive" runat="server" /><br/>
                    <asp:Button ID="btnLightBoxImage" runat="server" Text="Add Image" CssClass="SubmitButton" onclick="btnLightBoxImage_Click" />
                </fieldset>
                
                <hr />
                
                <asp:GridView ID="gvLightBoxImage"
                                DataKeyNames="LightboxImageId"
                                AutoGenerateColumns="false"
                                runat="server" 
                                onrowediting="gvLightBoxImage_RowEditing" 
                                onrowupdating="gvLightBoxImage_RowUpdating" 
                                onrowcancelingedit="gvLightBoxImage_RowCancelingEdit" onrowdeleting="gvLightBoxImage_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Image Filename">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Alt Text">
                            <ItemTemplate>
                                <asp:Label ID="lblAltTextImage" runat="server" Text='<%# Bind("AltText") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbAltTextImage" runat="server" Text='<%# Bind("AltText") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Image Width">
                            <ItemTemplate>
                                <asp:Label ID="lblWidth" runat="server" Text='<%# Bind("Width") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblWidth" runat="server" Text='<%# Bind("Width") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image Height">
                            <ItemTemplate>
                                <asp:Label ID="lblHeight" runat="server" Text='<%# Bind("Height") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblHeight" runat="server" Text='<%# Bind("Height") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Created">
                            <ItemTemplate>
                                <asp:Label ID="lblCreated" runat="server" Text='<%# Bind("Created") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCreated" runat="server" Text='<%# Bind("Created") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Is Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="lblActiveImage" Enabled="false" runat="server" Checked='<%# Bind("Active") %>'></asp:CheckBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cbActiveImage" runat="server" Checked='<%# Bind("Active") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this LightBox item?');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>