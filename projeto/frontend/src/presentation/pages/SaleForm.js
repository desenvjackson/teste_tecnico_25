import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  Typography,
  Container,
  TextField,
  Button,
  Grid,
  Card,
  CardContent,
  IconButton,
} from "@mui/material";
import { Add, Delete } from "@mui/icons-material";
import { Sale } from "../../domain/models/saleModel";
import { getSaleById, saveSale } from "../../application/services/saleService";

function SaleForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [sale, setSale] = useState(Sale.createEmpty());

  useEffect(() => {
    if (id) {
      getSaleById(id)
        .then((data) => setSale(data))
        .catch((error) => console.error("Erro ao buscar venda:", error));
    }
  }, [id]);

  const handleItemChange = (index, field, value) => {
    const updatedSale = new Sale(sale.clientId, sale.branch, [...sale.items]);
    updatedSale.updateItem(index, field, value);
    setSale(updatedSale);
  };

  const handleAddItem = () => {
    const updatedSale = new Sale(sale.clientId, sale.branch, [...sale.items]);
    updatedSale.addItem({
      productName: "",
      quantity: 0,
      unitPrice: 0,
      discount: 0,
    });
    setSale(updatedSale);
  };

  const handleRemoveItem = (index) => {
    const updatedSale = new Sale(sale.clientId, sale.branch, [...sale.items]);
    updatedSale.removeItem(index);
    setSale(updatedSale);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    saveSale(sale)
      .then(() => navigate("/"))
      .catch((error) => console.error("Erro ao salvar venda:", error));
  };

  return (
    <>
      {/* Cabeçalho */}
      <AppBar position="fixed" style={{ backgroundColor: "#1976d2" }}>
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            {id ? "Editar Venda" : "Criar Venda"}
          </Typography>
          <Button color="inherit" onClick={() => navigate("/")}>
            Voltar
          </Button>
        </Toolbar>
      </AppBar>

      {/* Espaço para compensar o cabeçalho fixo */}
      <Toolbar />

      {/* Formulário */}
      <Container maxWidth="md" style={{ marginTop: "20px" }}>
        <Card>
          <CardContent>
            <form onSubmit={handleSubmit}>
              <Typography variant="h5" gutterBottom>
                {id ? "Editar Venda" : "Criar Venda"}
              </Typography>
              <Grid container spacing={3}>
                <Grid item xs={12} sm={6}>
                  <TextField
                    label="Cliente"
                    fullWidth
                    value={sale.clientId}
                    onChange={(e) =>
                      setSale({ ...sale, clientId: e.target.value })
                    }
                  />
                </Grid>
                <Grid item xs={12} sm={6}>
                  <TextField
                    label="Filial"
                    fullWidth
                    value={sale.branch}
                    onChange={(e) =>
                      setSale({ ...sale, branch: e.target.value })
                    }
                  />
                </Grid>
              </Grid>

              <Typography variant="h6" style={{ marginTop: "20px" }}>
                Itens
              </Typography>
              {sale.items.map((item, index) => (
                <Grid
                  container
                  spacing={2}
                  key={index}
                  alignItems="center"
                  style={{ marginBottom: "10px" }}
                >
                  <Grid item xs={12} sm={4}>
                    <TextField
                      label="Produto"
                      fullWidth
                      value={item.productName}
                      onChange={(e) =>
                        handleItemChange(index, "productName", e.target.value)
                      }
                    />
                  </Grid>
                  <Grid item xs={12} sm={2}>
                    <TextField
                      label="Quantidade"
                      type="number"
                      fullWidth
                      value={item.quantity}
                      onChange={(e) =>
                        handleItemChange(
                          index,
                          "quantity",
                          parseInt(e.target.value, 10)
                        )
                      }
                    />
                  </Grid>
                  <Grid item xs={12} sm={2}>
                    <TextField
                      label="Preço Unitário"
                      type="number"
                      step="0.01"
                      fullWidth
                      value={item.unitPrice}
                      onChange={(e) =>
                        handleItemChange(
                          index,
                          "unitPrice",
                          parseFloat(e.target.value)
                        )
                      }
                    />
                  </Grid>
                  <Grid item xs={12} sm={2}>
                    <TextField
                      label="Desconto (%)"
                      type="number"
                      step="0.01"
                      fullWidth
                      value={item.discount * 100}
                      onChange={(e) =>
                        handleItemChange(
                          index,
                          "discount",
                          parseFloat(e.target.value) / 100
                        )
                      }
                    />
                  </Grid>
                  <Grid item xs={12} sm={2}>
                    <IconButton
                      color="secondary"
                      onClick={() => handleRemoveItem(index)}
                    >
                      <Delete />
                    </IconButton>
                  </Grid>
                </Grid>
              ))}
              <Button
                variant="outlined"
                startIcon={<Add />}
                onClick={handleAddItem}
                style={{ marginTop: "10px" }}
              >
                Adicionar Item
              </Button>
              <div style={{ marginTop: "20px" }}>
                <Button
                  type="submit"
                  variant="contained"
                  color="primary"
                  style={{ marginRight: "10px" }}
                >
                  Salvar
                </Button>
                <Button
                  variant="outlined"
                  color="secondary"
                  onClick={() => navigate("/")}
                >
                  Cancelar
                </Button>
              </div>
            </form>
          </CardContent>
        </Card>
      </Container>
    </>
  );
}

export default SaleForm;
