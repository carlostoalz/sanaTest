import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { DataView } from "primereact/dataview";
// import { addToCart, removeFromCart } from "../store/cartActions"; // Ejemplo de acciones de Redux
import {
  getLoading,
  getProducts,
} from "../../../store/selectors/catalog.selectors";
import ProductComponent from "./product.component";
import { ProductDTO } from "../../../models/ProductDTO.model";
import {
  addToCart,
  removeFromCart,
} from "../../../store/actions/shoppingCart.actions";

const ListComponent: React.FC = () => {
  const products = useSelector(getProducts);
  const loading = useSelector(getLoading);
  const dispatch = useDispatch();

  const [quantities, setQuantities] = useState<{ [key: number]: number }>({});

  const handleUpdateQuantity = (product: ProductDTO, newQuantity: number) => {
    setQuantities((prev) => ({ ...prev, [product.id]: newQuantity }));
    if (newQuantity > 0) {
      dispatch(addToCart({ ...product, quantity: newQuantity }));
    } else {
      dispatch(removeFromCart(product.id));
    }
  };

  const itemTemplate = (product: ProductDTO) => {
    const quantity = quantities[product.id] || 0;
    return (
      <ProductComponent
        product={product}
        quantity={quantity}
        onUpdateQuantity={(newQuantity) =>
          handleUpdateQuantity(product, newQuantity)
        }
      />
    );
  };

  return (
    <DataView
      value={products}
      itemTemplate={itemTemplate}
      rows={10}
      loading={loading}
      paginator
    />
  );
};

export default ListComponent;
