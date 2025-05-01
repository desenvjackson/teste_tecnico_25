import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SalesList from "./presentation/pages/SalesList";
import SaleForm from "./presentation/pages/SaleForm";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<SalesList />} />
        <Route path="/sales/new" element={<SaleForm />} />
        <Route path="/sales/:id/edit" element={<SaleForm />} />
      </Routes>
    </Router>
  );
}

export default App;
