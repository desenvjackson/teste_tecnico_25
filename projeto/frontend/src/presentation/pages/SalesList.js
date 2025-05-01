import React, { useEffect, useState } from "react";
import api from "../../infrastructure/api/apiClient";
import { Link } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  Typography,
  Container,
  Button,
  Card,
  CardContent,
  CardActions,
  List,
  ListItem,
  ListItemText,
  Grid,
  IconButton,
} from "@mui/material";
import { Delete } from "@mui/icons-material";
import { deleteSale } from "../../application/services/saleService";

function SalesList() {
  const [sales, setSales] = useState([]);
  const [expandedItems, setExpandedItems] = useState({});

  useEffect(() => {
    api
      .get("sales/all")
      .then((response) => setSales(response.data))
      .catch((error) => console.error("Erro ao buscar vendas:", error));
  }, []);

  const toggleItemsVisibility = (saleId) => {
    setExpandedItems((prevState) => ({
      ...prevState,
      [saleId]: !prevState[saleId],
    }));
  };

  const handleDelete = async (id) => {
    if (window.confirm("Tem certeza que deseja excluir esta venda?")) {
      try {
        await deleteSale(id);
        setSales((prevSales) => prevSales.filter((sale) => sale.id !== id));
      } catch (error) {
        console.error("Erro ao excluir venda:", error);
        alert("Não foi possível excluir a venda.");
      }
    }
  };

  return (
    <>
      <AppBar position="fixed" style={{ backgroundColor: "#1976d2" }}>
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            DeveloperStore
          </Typography>
          <Button color="inherit" component={Link} to="/sales/new">
            Criar Nova Venda
          </Button>
        </Toolbar>
      </AppBar>

      <Toolbar />

      <Container maxWidth="lg" style={{ marginTop: "20px" }}>
        <Typography variant="h4" gutterBottom>
          Vendas
        </Typography>
        <Grid container spacing={3} direction="row" wrap="wrap">
          {sales.map((sale) => (
            <Grid item xs={12} sm={6} md={4} key={sale.id}>
              <Card style={{ marginBottom: "20px" }}>
                <CardContent>
                  <Typography variant="h6">
                    Venda #{sale.id} - {sale.isCanceled ? "Cancelada" : "Ativa"}
                  </Typography>
                  <div
                    style={{
                      display: "flex",
                      justifyContent: "space-between",
                      flexWrap: "wrap",
                      gap: "10px",
                    }}
                  >
                    <Typography variant="subtitle2">
                      Cliente: {sale.client}
                    </Typography>
                    <Typography variant="subtitle2">
                      Filial: {sale.branch}
                    </Typography>
                    <Typography variant="subtitle2">
                      Data: {new Date(sale.saleDate).toLocaleDateString()}
                    </Typography>
                    <Typography variant="subtitle2">
                      Valor Total: R$ {sale.totalValue.toFixed(2)}
                    </Typography>
                  </div>
                  <Typography
                    variant="subtitle1"
                    style={{
                      marginTop: "10px",
                      cursor: "pointer",
                      color: "#1976d2",
                    }}
                    onClick={() => toggleItemsVisibility(sale.id)}
                  >
                    Itens {expandedItems[sale.id] ? "▲" : "▼"}
                  </Typography>
                  {expandedItems[sale.id] && (
                    <List>
                      {sale.items.map((item) => (
                        <ListItem key={item.id} disableGutters>
                          <ListItemText
                            primary={`Produto: ${item.productName || "N/A"}`}
                            secondary={
                              <>
                                <Typography variant="body2">
                                  Quantidade: {item.quantity}
                                </Typography>
                                <Typography variant="body2">
                                  Preço Unitário: R$ {item.unitPrice.toFixed(2)}
                                </Typography>
                                <Typography variant="body2">
                                  Desconto: {item.discount * 100}%
                                </Typography>
                                <Typography variant="body2">
                                  Valor Total: R$ {item.totalValue.toFixed(2)}
                                </Typography>
                              </>
                            }
                          />
                        </ListItem>
                      ))}
                    </List>
                  )}
                </CardContent>
                <CardActions>
                  <Button
                    size="small"
                    variant="outlined"
                    color="primary"
                    component={Link}
                    to={`/sales/${sale.id}/edit`}
                  >
                    Editar Venda
                  </Button>
                  <IconButton
                    size="small"
                    color="secondary"
                    onClick={() => handleDelete(sale.id)}
                  >
                    <Delete />
                  </IconButton>
                </CardActions>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
    </>
  );
}

export default SalesList;