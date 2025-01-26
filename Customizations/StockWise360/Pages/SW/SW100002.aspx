<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SW100002.aspx.cs" Inherits="Page_SW100002" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="StockWise360.BLC.SWCollectionTargetMaint"
        PrimaryView="CollectionTargetView"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="CollectionTargetView" Width="100%" Height="100px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="CstPXNumberEdit4" DataField="CollectionTargetID" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit1" DataField="CollectionName" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="CollectionPath" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit3" DataField="MainPrompt" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="CollectionTargetQuestionView">
			    <Columns>
			        
			    </Columns>
			
				<RowTemplate>
					<px:PXTextEdit runat="server" ID="CstPXTextEdit5" DataField="QuestionText" />
					<px:PXTextEdit runat="server" ID="CstPXTextEdit6" DataField="ResultSample" /></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>