import { RootState } from "../store";

export const getCarItems = (state: RootState) => state.shoppingCart.cartItems;
