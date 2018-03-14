<%@ Page Title="Reporte de Comisiones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComisionesReport.aspx.cs" Inherits="Calculo_Comisiones_Operadores.ComisionesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--------------------------- PANTALLA DE COMISIONES ---------------------------%>
    <h1><b><%: Title %></b>
    </h1>
    <br />
    &nbsp
    <div class="tab-content" align="center">
        <div class="tab-panel">

            <div class="panel-heading form-inline" style="background-color: gray; height: 52px;">
                <div class="form-group pull-left">
                    <label for="Opcion">Fecha: &nbsp</label>
                    <asp:TextBox ID="txtDateStart" runat="server" Font-Bold="true" TextMode="Date"></asp:TextBox>
                    <label for="Opcion">-</label>
                    <asp:TextBox ID="txtDateEnd" runat="server" Font-Bold="true" TextMode="Date"></asp:TextBox>
                    &nbsp
                    <label for="Opcion">|</label>
                    &nbsp
                    <label for="ckbSummary">Tipo de Reporte:&nbsp</label>
                    <asp:CheckBox ID="ckbSummary" Text="Resumen" runat="server" />
                    &nbsp
                    <asp:CheckBox ID="ckbDetail" Text="Detallado" runat="server"/>
                </div>
                <div class="form-group pull-right">
                    <asp:ImageButton ID="btnCreate" runat="server" ControlStyle-CssClass="btn btn-success" ImageUrl="Imagenes/add.png" OnClick="btnCreate_Click" ToolTip="Crear Reporte"></asp:ImageButton>
                    <%-- OnClick="btnAdd_Click"--%>
                </div>
            </div>
            <br />
            <%--<asp:Timer ID="timer" runat="server" OnTick="timer_Tick"></asp:Timer>--%>
            <asp:GridView ID="dgvComisiones" runat="server" Width="100%" AllowPaging="true" OnPageIndexChanging="dgvComisiones_PageIndexChanging" AutoGenerateColumns="false" DataKeyNames="scco_id" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" ForeColor="Black" CssClass="table table-hover table-striped" OnRowCommand="dgvComisiones_RowCommand" HorizontalAlign="Center">
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000" Font-Bold="True" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />

                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:ButtonField DataTextField="scco_id" HeaderText=" ID" />
                    <asp:ButtonField DataTextField="scco_name" HeaderText=" Nombre" />
                    <asp:ButtonField DataTextField="scco_type" HeaderText=" Reporte" />
                    <asp:ButtonField DataTextField="scco_size" HeaderText=" Peso" />
                    <asp:ButtonField DataTextField="scco_date" HeaderText=" Fecha" />
                    <%--<asp:ButtonField DataTextField="scco_hora" HeaderText=" Hora" />--%>
                    <%--<asp:ButtonField CommandName="viewRecord" ControlStyle-CssClass="btn btn-info" ButtonType="Image" ImageUrl="Imagenes/user.png" HeaderText=" Detalle"></asp:ButtonField>--%>
                    <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-primary" ButtonType="Image" ImageUrl="Imagenes/download_icon.png" HeaderText=" Descargar"></asp:ButtonField>
                    <asp:ButtonField CommandName="deleteRecords" ControlStyle-CssClass="btn btn-danger" ButtonType="Image" ImageUrl="imagenes/trash.png" HeaderText=" Eliminar"></asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%--------------------------- MODAL DE REPORTE ELIMINAR---------------------------%>
    <div id="viewModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="viewModalLabel">Reporte - Eliminar</h3>
                </div>
                <asp:UpdatePanel ID="viewPanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <asp:HiddenField ID="deleteID" runat="server" />
                            <label>¿Desea usted, eliminar este reporte?</label>
                            <%--<asp:DetailsView ID="detailsView" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="false">--%>
                                <%--<Fields>--%>
                                    <%--<asp:BoundField DataField="scco_user" HeaderText="Usuario: " />--%>
                                    <%--<asp:BoundField DataField="scco_name" HeaderText="Nombre: " />
                                    <asp:BoundField DataField="scco_lastname" HeaderText="Apellido Paterno: " />
                                    <asp:BoundField DataField="scco_mlastname" HeaderText="Apellido Materno: " />
                                    <asp:BoundField DataField="scco_age" HeaderText="Edad: " />
                                    <asp:BoundField DataField="scco_sex" HeaderText="Genero: " />
                                    <asp:BoundField DataField="scco_birthdate" HeaderText="Nacimiento: " />
                                    <asp:BoundField DataField="scco_lblstatus" HeaderText="Status: " />--%>
                                <%--</Fields>--%>
                            <%--</asp:DetailsView>--%>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAdd" runat="server" Text="Eliminar" CssClass="btn btn-success" OnClick="btnDelete_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvComisiones" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE REPORTE ELIMINAR---------------------------%>
    <div id="updateModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="updateModalLabel">Reporte - Descargar</h3>
                </div>
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <asp:HiddenField ID="DownId" runat="server" />
                            <label>¿Desea usted, descargar este reporte?</label>
                            <asp:Label runat="server" ID="lbFile"></asp:Label>
                            <%--<asp:DetailsView ID="detailsView" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="false">--%>
                                <%--<Fields>--%>
                                    <%--<asp:BoundField DataField="scco_user" HeaderText="Usuario: " />--%>
                                    <%--<asp:BoundField DataField="scco_name" HeaderText="Nombre: " />
                                    <asp:BoundField DataField="scco_lastname" HeaderText="Apellido Paterno: " />
                                    <asp:BoundField DataField="scco_mlastname" HeaderText="Apellido Materno: " />
                                    <asp:BoundField DataField="scco_age" HeaderText="Edad: " />
                                    <asp:BoundField DataField="scco_sex" HeaderText="Genero: " />
                                    <asp:BoundField DataField="scco_birthdate" HeaderText="Nacimiento: " />
                                    <asp:BoundField DataField="scco_lblstatus" HeaderText="Status: " />--%>
                                <%--</Fields>--%>
                            <%--</asp:DetailsView>--%>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDown" runat="server" Text="Si" CssClass="btn btn-success" OnClick="btnDown_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvComisiones" EventName="RowCommand" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnDown" EventName="Click" />--%>
                        <asp:PostBackTrigger ControlID="btnDown"/>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
