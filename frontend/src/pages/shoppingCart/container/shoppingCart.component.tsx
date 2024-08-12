import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { getCarItems } from "../../../store/selectors/shoppingCart.selectors";
import FormComponent from "../components/form.component";
import { removeFromCar } from "../../../store/reducers/shoppingCart.reducer";

export const ShoppingCartComponent: React.FC = () => {
  const carItems = useSelector(getCarItems);
  const dispatch = useDispatch();

  const handleDelete = (productId: number) => {
    dispatch(removeFromCar(productId));
  };

  const total = carItems
    .map((item) => item.quantity * item.price)
    .reduce((acum, item) => acum + item, 0);
  return (
    <div className="row">
      <div className="col-8">
        <table className="table">
          <thead>
            <tr>
              <th scope="col">Product</th>
              <th scope="col">Price</th>
              <th scope="col">Quantity</th>
              <th scope="col">Total</th>
            </tr>
          </thead>
          <tbody>
            {carItems.map((product, index) => (
              <tr key={index}>
                <th scope="row">
                  <div className="row">
                    <div className="col-1 d-flex align-items-center">
                      <img className="img-fluid" src={product.img} alt="" />
                    </div>
                    <div className="col-11">
                      <h5 className="mb-1">{product.name}</h5>
                      <p>
                        Item No. {product.code} | {product.stock} in stock
                      </p>
                    </div>
                    <div className="col-12">
                      <button
                        type="button"
                        className="btn ms-5 ps-5"
                        onClick={() => handleDelete(product.id)}
                      >
                        <i className="fa-solid fa-trash-can"></i>
                        Delete
                      </button>
                    </div>
                  </div>
                </th>
                <td className="text-nowrap">$ {product.price}</td>
                <td className="text-nowrap">{product.quantity}</td>
                <td className="text-nowrap">
                  $ {product.price * product.quantity}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div
        className="col-4 d-flex flex-column border-start border-2"
        style={{ height: "93vh" }}
      >
        <h1 className="mb-5 pb-5">Shopping cart details</h1>
        <div className="row pe-3">
          <div className="col-12 d-flex justify-content-between">
            <span>
              Items ({carItems.reduce((acum, item) => acum + item.quantity, 0)})
              units
            </span>
            <span>$ {total}</span>
          </div>
          <hr className="border-1" />
          <div className="col-12 d-flex justify-content-between">
            <span>Total</span>
            <span>$ {total * 1.19}</span>
          </div>
          <hr className="border-1" />
          <FormComponent />
        </div>
      </div>
    </div>
  );
};

export default ShoppingCartComponent;
