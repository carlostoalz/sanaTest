import React, { useState } from "react";
import { ProductDTO } from "../../../models/ProductDTO.model";

interface ProductComponentProps {
  product: ProductDTO;
  quantity: number;
  onUpdateQuantity: (newQuantity: number) => void;
}

const ProductComponent: React.FC<ProductComponentProps> = ({
  product,
  quantity,
  onUpdateQuantity,
}) => {
  const [inputQuantity, setInputQuantity] = useState(quantity);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseInt(e.target.value, 10);
    onUpdateQuantity(isNaN(value) ? 0 : value);
    setInputQuantity(isNaN(value) ? 0 : value);
  };

  const handleIncrease = () => {
    const newQuantity = inputQuantity + 1;
    if (newQuantity <= product.stock) {
      onUpdateQuantity(newQuantity);
      setInputQuantity(newQuantity);
    }
  };

  const handleDecrease = () => {
    const newQuantity = inputQuantity - 1;
    if (newQuantity >= 0) {
      onUpdateQuantity(newQuantity);
      setInputQuantity(newQuantity);
    }
  };

  return (
    <div className="row mb-2" key={product.id}>
      <div className="col-1 d-flex align-items-center">
        <img className="img-fluid" src={product.img} alt="" />
      </div>
      <div className="col-11">
        <div className="row d-flex justify-content-between align-items-center">
          <div className="col-8">
            <h4>{product.name}</h4>
            <p>
              Item No. {product.code} | {product.stock} in stock
            </p>
            <p>{product.description}</p>
          </div>
          <div className="col-2 d-flex justify-content-end">
            ${product.price}
          </div>
          <div className="col-2 d-flex align-items-center">
            <div className="input-group pe-4">
              <button
                className="btn btn-outline-secondary"
                type="button"
                onClick={handleDecrease}
              >
                -
              </button>
              <input
                type="number"
                className="form-control"
                min={0}
                max={product.stock}
                value={inputQuantity}
                onChange={handleInputChange}
              />
              <button
                className="btn btn-outline-secondary"
                type="button"
                onClick={handleIncrease}
              >
                +
              </button>
            </div>
          </div>
        </div>
      </div>
      <hr className="border-3" />
    </div>
  );
};

export default ProductComponent;
