<%@ Page Title="Unidad Administrador" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnidadAdmin.aspx.cs" Inherits="Calculo_Comisiones_Operadores.UnidadAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--------------------------- PANTALLA DE UNIDAD ---------------------------%>
    <h1><b><%: Title %></b>
    </h1>
    <br />
    &nbsp
    <div class="tab-content" align="center">
        <div class="tab-panel">

            <div class="panel-heading form-inline" style="background-color: gray; height: 52px;">
                <div class="form-group pull-left">
                    <label for="Opcion">Buscar: &nbsp</label>
                    <asp:TextBox ID="txtSearch" runat="server" Width="277px"></asp:TextBox>
                    <asp:ImageButton ID="btnSearch" runat="server" ControlStyle-CssClass="btn btn-primary" ImageUrl="Imagenes/search.png" OnClick="btnSearch_Click" ToolTip="Buscar"></asp:ImageButton>
                    <%--OnClick="btnSearch_Click"--%>
                </div>
                <div class="form-group pull-right">
                    <asp:ImageButton ID="btnNew" runat="server" ControlStyle-CssClass="btn btn-success" ImageUrl="Imagenes/add.png" OnClick="btnNew_Click" ToolTip="Agregar Unidad"></asp:ImageButton>
                    <%-- OnClick="btnAdd_Click"--%>
                </div>
            </div>
            <br />
            <%--<asp:Timer ID="timer" runat="server" OnTick="timer_Tick"></asp:Timer>--%>
            <asp:GridView ID="dgvUnidad" runat="server" Width="70%" AllowPaging="true" OnPageIndexChanging="dgvUnidad_PageIndexChanging" AutoGenerateColumns="false" DataKeyNames="scco_id" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" ForeColor="Black" CssClass="table table-hover table-striped" OnRowCommand="dgvUnidad_RowCommand" HorizontalAlign="Center">
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
                    <asp:ButtonField DataTextField="scco_name" HeaderText=" Unidad" />
                    <%--<asp:ButtonField DataTextField="scco_namelastname" HeaderText=" Usuario" />--%>
                    <asp:ButtonField CommandName="viewRecord" ControlStyle-CssClass="btn btn-info" ButtonType="Image" ImageUrl="Imagenes/user.png" HeaderText=" Detalle"></asp:ButtonField>
                    <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-warning" ButtonType="Image" ImageUrl="Imagenes/edit.png" HeaderText=" Modificar"></asp:ButtonField>
                    <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-danger" ButtonType="Image" ImageUrl="imagenes/trash.png" HeaderText=" Eliminar"></asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%--------------------------- MODAL DE UNIDAD DETALLE---------------------------%>
    <div id="viewModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="viewModalLabel">Unidad - Detalle</h3>
                </div>
                <asp:UpdatePanel ID="viewPanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <asp:DetailsView ID="detailsView" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="false">
                                <Fields>
                                    <%--<asp:BoundField DataField="scco_user" HeaderText="Usuario: " />--%>
                                    <asp:BoundField DataField="scco_name" HeaderText="Unidad: " />
                                    <asp:BoundField DataField="scco_modelo" HeaderText="Modelo: " />
                                    <asp:BoundField DataField="scco_placa" HeaderText="Placa: " />
                                    <asp:BoundField DataField="scco_lblstatus" HeaderText="Status: " />
                                </Fields>
                            </asp:DetailsView>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvUnidad" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

      <%--------------------------- MODAL DE UNIDAD AGREGAR---------------------------%>
    <div id="addModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="addModalLabel">Unidad - Agregar</h3>
                </div>
                <asp:UpdatePanel ID="updatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:HiddenField ID="txtIdAdd" runat="server" />
                                <table class="table">
                                    <tr>
                                        <td>Nombre: </td>
                                        <td>
                                            <asp:TextBox ID="txtNameAdd" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Modelo: </td>
                                        <td>
                                            <asp:TextBox ID="txtModeloAdd" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Placa: </td>
                                        <td>
                                            <asp:TextBox ID="txtPlacaAdd" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Status: </td>
                                        <td>
                                            <asp:CheckBox ID="ckbStatusAdd" runat="server" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvUnidad" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE UNIDAD ACTUALIZAR---------------------------%>
    <div id="editModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="editModalLabel">Unidad - Actualizar</h3>
                </div>
                <asp:UpdatePanel ID="updatePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:HiddenField ID="txtIdMod" runat="server" />
                                <table class="table">
                                    <tr>
                                        <td>Nombre: </td>
                                        <td>
                                            <asp:TextBox ID="txtNameMod" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Modelo: </td>
                                        <td>
                                            <asp:TextBox ID="txtModeloMod" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Placa: </td>
                                        <td>
                                            <asp:TextBox ID="txtPlacaMod" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Status: </td>
                                        <td>
                                            <asp:CheckBox ID="ckbStatusMod" runat="server"/></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvUnidad" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

     <%--------------------------- MODAL DE UNIDAD ELIMINAR---------------------------%>
    <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="delModalLabel">Unidad - Eliminar</h3>
                </div>
                <asp:UpdatePanel ID="deletePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Esta seguro que desea eliminar ha esta unidad?
                    <asp:HiddenField ID="deleteID" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-success" OnClick="btnDelete_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
