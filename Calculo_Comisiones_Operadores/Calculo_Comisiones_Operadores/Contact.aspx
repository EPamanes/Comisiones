<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Calculo_Comisiones_Operadores.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>E.G.P.</h3>
    <address>
        <%--One Microsoft Way<br />
        Redmond, WA 98052-6399<br />--%>
        <abbr title="Telefono">Tel:</abbr>
       871-460-6046
    </address>

    <address>
        <strong>Soporte:</strong>   <a href="e.g.pamanes@gmail.com">e.g.pamanes@gmail.com</a><br />
        <%--<strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>--%>
    </address>
</asp:Content>
