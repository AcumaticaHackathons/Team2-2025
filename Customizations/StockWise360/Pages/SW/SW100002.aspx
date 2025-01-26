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
			<px:PXLayoutRule GroupCaption="Settings" runat="server" ID="CstPXLayoutRule14" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="CstPXNumberEdit4" DataField="CollectionTargetID" NullText="<NEW>" ></px:PXSelector>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit1" DataField="CollectionName" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="CollectionPath" ></px:PXTextEdit>
			<px:PXLayoutRule ControlSize="XXL" runat="server" ID="CstPXLayoutRule15" StartColumn="True" GroupCaption="Main Prompt" SuppressLabel="True" ></px:PXLayoutRule>
			<px:PXTextEdit Width="300px" Size="XXL" Height="300px" TextMode="MultiLine" runat="server" ID="CstPXTextEdit3" DataField="MainPrompt" ></px:PXTextEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule16" StartColumn="True" GroupCaption="Question" SuppressLabel="True" ></px:PXLayoutRule>
			<px:PXTextEdit Width="300px" Height="300px" TextMode="MultiLine" runat="server" ID="CstPXTextEdit12" DataField="Question" ></px:PXTextEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule17" StartColumn="True" SuppressLabel="True" GroupCaption="Sample Result" ></px:PXLayoutRule>
			<px:PXTextEdit Width="300px" Height="300px" TextMode="MultiLine" runat="server" ID="CstPXTextEdit13" DataField="SampleResult" ></px:PXTextEdit>
			<px:PXJavaScript runat="server" ID="CstJavaScript19" ></px:PXJavaScript>
			<px:PXJavaScript runat="server" ID="CstJavaScript20" ></px:PXJavaScript>
			<px:PXJavaScript runat="server" ID="CstJavaScript21" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid AutoAdjustColumns="True" ID="grid" runat="server" DataSourceID="ds" Height="150px" SkinID="Details" AllowAutoHide="false" Width="100%">
		<Levels>
			<px:PXGridLevel DataMember="CollectionTargetQuestionView">
			    <Columns>
				<px:PXGridColumn AllowFilter="True" Type="Icon" DataField="ThumbnailURL" Width="100"></px:PXGridColumn>
				<px:PXGridColumn DataField="ItemID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Manufacturer" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Information" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Description" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Vendors" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Use" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Lead" Width="70" ></px:PXGridColumn></Columns>
					
				<RowTemplate>
					<px:PXTextEdit Height="300px" TextMode="MultiLine" runat="server" ID="CstPXTextEdit7" DataField="QuestionText" ></px:PXTextEdit></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	
		<Mode InitNewRow="True" ></Mode>
		<Mode AllowFormEdit="True" ></Mode></px:PXGrid>
	<px:PXJavaScript runat="server" ID="CstJavaScript22" Script="var css = &#39;.GridRow > img { width: 150px; height: 150px; }&#39;, head = document.head || document.getElementsByTagName(&#39;head&#39;)[0], style = document.createElement(&#39;style&#39;); style.type = &#39;text/css&#39;; if (style.styleSheet){   style.styleSheet.cssText = css; } else { style.appendChild(document.createTextNode(css)); }  head.appendChild(style);" ></px:PXJavaScript></asp:Content>