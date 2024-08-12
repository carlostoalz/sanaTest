import { createSlice, PayloadAction, SerializedError } from "@reduxjs/toolkit";
import { ProductDTO } from "../../models/ProductDTO.model";
import { createOrder } from "../actions/shoppingCart.actions";

interface ShoppingCartState {
  cartItems: ProductDTO[];
  loading: boolean;
  error: SerializedError;
}

const initialState: ShoppingCartState = {
  cartItems: [],
  loading: false,
  error: null,
};

export const shoppingCarSlice = createSlice({
  name: "shoppingCar",
  initialState,
  reducers: {
    addToCar: (state, action: PayloadAction<ProductDTO>) => ({
      ...state,
      cartItems: state.cartItems
        .filter((item) => item.id !== action.payload.id)
        .concat(action.payload),
    }),
    removeFromCar: (state, action: PayloadAction<number>) => ({
      ...state,
      cartItems: state.cartItems.filter((item) => item.id !== action.payload),
    }),
    clearCar: (state) => ({
      ...state,
      cartItems: [],
    }),
  },
  extraReducers: (builder) => {
    builder
      .addCase(createOrder.pending, (state) => ({
        ...state,
        loading: true,
      }))
      .addCase(createOrder.fulfilled, (state) => ({
        ...state,
        loading: false,
      }));
  },
});
export const { addToCar, removeFromCar, clearCar } = shoppingCarSlice.actions;
export default shoppingCarSlice.reducer;
