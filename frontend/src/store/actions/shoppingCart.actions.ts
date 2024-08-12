import { ProductDTO } from "../../models/ProductDTO.model";

export const ADD_TO_CART = "ADD_TO_CART";
export const REMOVE_FROM_CART = "REMOVE_FROM_CART";

export const addToCart = (product: ProductDTO) => ({
  type: ADD_TO_CART,
  payload: product,
});

export const removeFromCart = (productId: number) => ({
  type: REMOVE_FROM_CART,
  payload: productId,
});
