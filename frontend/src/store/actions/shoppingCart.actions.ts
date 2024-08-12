import { createAsyncThunk } from "@reduxjs/toolkit";
import { OrderDTO } from "../../models/OrderDTO.model";
import axiosClient from "../../api/client";

export const createOrder = createAsyncThunk(
  "shoppingCar/createOrder",
  async (order: OrderDTO) => {
    await axiosClient.post("orders", order);
  }
);
