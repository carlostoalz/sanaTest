import React, { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { v4 as uuidv4 } from "uuid";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { OrderDTO } from "../../../models/OrderDTO.model";
import { getCarItems } from "../../../store/selectors/shoppingCart.selectors";
import { AppDispatch } from "../../../store/store";
import { createOrder } from "../../../store/actions/shoppingCart.actions";
import { clearCar } from "../../../store/reducers/shoppingCart.reducer";

const FormComponent: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const carItems = useSelector(getCarItems);
  const navigate = useNavigate();
  const [order, setOrder] = useState<OrderDTO>({
    Customer: {
      Id: 0,
      Code: uuidv4(),
      Name: null,
      Email: null,
    },
    Order: {
      Id: 0,
      Id_customer: 0,
      Total_price:
        carItems
          .map((item) => item.quantity * item.price)
          .reduce((acum, item) => acum + item, 0) * 1.19,
      Total_products: carItems.reduce((acum, item) => acum + item.quantity, 0),
    },
    OrderProducts: carItems.map((item) => ({
      Id: 0,
      Id_order: 0,
      Id_product: item.id,
      Quantity: item.quantity,
    })),
  });
  const [isFormValid, setIsFormValid] = useState<boolean>(false);
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setOrder({
      ...order,
      Customer: { ...order.Customer, [name]: value },
    });
  };
  const handleSubmit = (event: FormEvent) => {
    event.preventDefault();
    dispatch(createOrder(order));
    dispatch(clearCar());
    navigate("/");
  };

  useEffect(() => {
    const { Name, Email } = order.Customer;
    setIsFormValid(Name?.trim() !== "" && Email?.trim() !== "");
  }, [order]);

  return (
    <form className="row" onSubmit={handleSubmit}>
      <div className="col-md-12">
        <label className="form-label">Name</label>
        <input
          type="text"
          className="form-control"
          name="Name"
          onChange={handleChange}
          required
        />
      </div>
      <div className="col-md-12">
        <label className="form-label">Email</label>
        <input
          type="email"
          className="form-control"
          name="Email"
          onChange={handleChange}
          required
        />
      </div>
      <div className="col-md-12 mt-3">
        <button
          type="submit"
          className="btn btn-primary text-nowrap d-flex justify-content-center w-100"
          disabled={!isFormValid}
        >
          Proccess Order
          <i className="fa-solid fa-chevron-right align-self-center ps-5"></i>
        </button>
      </div>
    </form>
  );
};

export default FormComponent;
