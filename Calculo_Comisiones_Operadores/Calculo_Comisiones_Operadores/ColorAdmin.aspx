<%@ Page Title="Color Administrador" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ColorAdmin.aspx.cs" Inherits="Calculo_Comisiones_Operadores.ColorAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--------------------------- PANTALLA DE COLOR ---------------------------%>
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
                    <asp:ImageButton ID="btnNew" runat="server" ControlStyle-CssClass="btn btn-success" ImageUrl="Imagenes/add.png" OnClick="btnNew_Click" ToolTip="Agregar Color"></asp:ImageButton>
                    <%-- OnClick="btnAdd_Click"--%>
                </div>
            </div>
            <br />
            <%--<asp:Timer ID="timer" runat="server" OnTick="timer_Tick"></asp:Timer>--%>
            <asp:GridView ID="dgvColor" runat="server" Width="70%" AllowPaging="true" OnPageIndexChanging="dgvColor_PageIndexChanging" AutoGenerateColumns="false" DataKeyNames="scco_id" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" ForeColor="Black" CssClass="table table-hover table-striped" OnRowCommand="dgvColor_RowCommand" HorizontalAlign="Center">
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
                    <asp:ButtonField DataTextField="scco_name" HeaderText=" Color" />
                    <%--<asp:ButtonField DataTextField="scco_hora" HeaderText=" Hora" />--%>
                    <asp:ButtonField CommandName="viewRecord" ControlStyle-CssClass="btn btn-info" ButtonType="Image" ImageUrl="Imagenes/user.png" HeaderText=" Detalle"></asp:ButtonField>
                    <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-warning" ButtonType="Image" ImageUrl="Imagenes/edit.png" HeaderText=" Modificar"></asp:ButtonField>
                    <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-danger" ButtonType="Image" ImageUrl="imagenes/trash.png" HeaderText=" Eliminar"></asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%--------------------------- MODAL DE COLOR DETALLE---------------------------%>
    <div id="viewModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" style="width:500px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="viewModalLabel">Color - Detalle</h3>
                </div>
                <asp:UpdatePanel ID="viewPanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <asp:DetailsView ID="detailsView" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="false">
                                <Fields>
                                    <%--<asp:BoundField DataField="scco_user" HeaderText="Usuario: " />--%>
                                    <asp:BoundField DataField="scco_name" HeaderText="Color: " />
                                    <asp:BoundField DataField="scco_hora" HeaderText="Hora: " />
                                    <asp:BoundField DataField="scco_prioridad" HeaderText="Prioridad: " />
                                    <asp:BoundField DataField="scco_tarifa" HeaderText="Tarifa: " />
                                </Fields>
                            </asp:DetailsView>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvColor" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE COLOR AGREGAR---------------------------%>
    <div id="addModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" style="width:500px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="addModalLabel">Color - Agregar</h3>
                </div>
                <asp:UpdatePanel ID="updatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div class="container-fluid">
                                    <asp:HiddenField ID="txtIdAdd" runat="server" />
                                    <div class="row">
                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Color:</asp:Label>
                                            <asp:TextBox ID="txtNameAdd" runat="server" CssClass="form-control" Style="text-align: center" Font-Bold="true"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Hora:</asp:Label>
                                            <asp:TextBox ID="txtHoraAdd" runat="server" CssClass="form-control" Style="text-align: center" Font-Bold="true" MaximumValue="23:59:59" MinimumValue="00:00:00"></asp:TextBox>
                                            <%--<asp:Timer runat="server" ID="timeHoraAdd"></asp:Timer>--%>
                                            <ajaxToolkit:MaskedEditExtender ID="maskHoraAdd" runat="server" Mask="99:99:99" TargetControlID="txtHoraAdd" MaskType="Time" AcceptNegative="None" ErrorTooltipEnabled="true" />
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Tarifa:</asp:Label>
                                            <asp:TextBox ID="txtTarifaAdd" runat="server" CssClass="form-control" Style="text-align: center;" Font-Bold="true" TextMode="Number" step="0.01"></asp:TextBox>
                                            <%--<ajaxToolkit:MaskedEditExtender ID="maskTarifaAdd" runat="server" Mask="9,999,999.99" TargetControlID="txtTarifaAdd" MaskType="Number" AcceptNegative="None" ErrorTooltipEnabled="true" />--%>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Prioridad:</asp:Label>
                                            <asp:DropDownList runat="server" ID="listPrioridadAdd" CssClass="dropbtn" Font-Bold="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvColor" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE COLOR ACTUALIZAR---------------------------%>
    <div id="editModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" style="width:500px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="editModalLabel">Color - Actualizar</h3>
                </div>
                <asp:UpdatePanel ID="updatePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div class="container-fluid">
                                    <asp:HiddenField ID="txtIdMod" runat="server" />
                                    <div class="row">
                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Color:</asp:Label>
                                            <asp:TextBox ID="txtNameMod" runat="server" CssClass="form-control" Style="text-align: center" Font-Bold="true"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Hora:</asp:Label>
                                            <asp:TextBox ID="txtHoraMod" runat="server" CssClass="form-control" Style="text-align: center" Font-Bold="true" MaximumValue="23:59:59" MinimumValue="00:00:00"></asp:TextBox>
                                            <%--<asp:Timer runat="server" ID="timeHoraAdd"></asp:Timer>--%>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99:99:99" TargetControlID="txtHoraMod" MaskType="Time" AcceptNegative="None" ErrorTooltipEnabled="true" />
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Tarifa:</asp:Label>
                                            <asp:TextBox ID="txtTarifaMod" runat="server" CssClass="form-control" Style="text-align: center;" Font-Bold="true" TextMode="Number" step="0.01"></asp:TextBox>
                                            <%--<ajaxToolkit:MaskedEditExtender ID="maskTarifaAdd" runat="server" Mask="9,999,999.99" TargetControlID="txtTarifaAdd" MaskType="Number" AcceptNegative="None" ErrorTooltipEnabled="true" />--%>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-3 control-label" Font-Bold="true" Style="text-align: right;">Prioridad:</asp:Label>
                                            <asp:DropDownList runat="server" ID="listPrioridadMod" CssClass="dropbtn" Font-Bold="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvColor" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE COLOR ELIMINAR---------------------------%>
    <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="delModalLabel">Color - Eliminar</h3>
                </div>
                <asp:UpdatePanel ID="deletePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Esta seguro que desea eliminar este color?
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
