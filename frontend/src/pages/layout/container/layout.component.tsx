import React, { Suspense } from "react";
import NavBarComponent from "../components/nav-bar.component";
import { Route, Routes } from "react-router-dom";
import { ScrollPanel } from "primereact/scrollpanel";

const LayoutComponent: React.FC = () => {
  const Catalog = React.lazy(
    () => import("../../catalog/container/catalog.component")
  );
  const ShoppingCart = React.lazy(
    () => import("../../shoppingCart/container/shoppingCart.component")
  );
  return (
    <>
      <NavBarComponent />
      <ScrollPanel style={{ width: "100%", height: "93vh", marginTop: "56px" }}>
        <Suspense fallback={<div>Espere por favor...</div>}>
          <Routes>
            <Route path="/" element={<Catalog />} />
            <Route path="/shoppingCart" element={<ShoppingCart />} />
          </Routes>
        </Suspense>
      </ScrollPanel>
    </>
  );
};

export default LayoutComponent;
