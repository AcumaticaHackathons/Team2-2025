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
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="CollectionTargetView"  AllowAutoHide="false" Width="100%">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="CstPXNumberEdit4" DataField="CollectionTargetID" NullText="<NEW>" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit1" DataField="CollectionName" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="CollectionPath" />
			<px:PXTextEdit runat="server" ID="CstPXTextEdit3" DataField="MainPrompt" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid AutoAdjustColumns="True" ID="grid" runat="server" DataSourceID="ds" Height="150px" SkinID="Details" AllowAutoHide="false" Width="100%">
		<Levels>
			<px:PXGridLevel DataMember="CollectionTargetQuestionView">
			    <Columns>
				<px:PXGridColumn DataField="QuestionText" Width="280" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ResultSample" Width="280" ></px:PXGridColumn></Columns>
					
				<RowTemplate>
										<px:PXLayoutRule runat="server" ID="CstPXLayoutRule11" StartRow="True" ></px:PXLayoutRule>
										<px:PXLayoutRule ColumnWidth="40%" runat="server" ID="CstPXLayoutRule9" StartColumn="True" ></px:PXLayoutRule>
					<px:PXTextEdit Height="300px" TextMode="MultiLine" runat="server" ID="CstPXTextEdit7" DataField="QuestionText" ></px:PXTextEdit>
										<px:PXLayoutRule runat="server" ID="CstPXLayoutRule10" StartColumn="True" ColumnWidth="" ></px:PXLayoutRule>
					<px:PXTextEdit TextMode="MultiLine" Height="300px" runat="server" ID="CstPXTextEdit8" DataField="ResultSample" ></px:PXTextEdit></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	
		<Mode InitNewRow="True" ></Mode>
		<Mode AllowFormEdit="True" ></Mode></px:PXGrid>
</asp:Content>