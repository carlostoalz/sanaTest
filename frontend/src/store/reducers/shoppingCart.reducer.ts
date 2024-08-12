import { ProductDTO } from "../../models/ProductDTO.model";
import { ADD_TO_CART, REMOVE_FROM_CART } from "../actions/shoppingCart.actions";

interface ShoppingCartState {
  cartItems: ProductDTO[];
}

const initialState: ShoppingCartState = {
  cartItems: [],
};

const shoppingCartReducer = (
  state = initialState,
  action: { type: any; payload: number | ProductDTO }
) => {
  switch (action.type) {
    case ADD_TO_CART:
      return {
        ...state,
        cartItems: state.cartItems
          .filter((item) => item.id !== (action.payload as ProductDTO).id)
          .concat(action.payload as ProductDTO),
      };
    case REMOVE_FROM_CART:
      return {
        ...state,
        cartItems: state.cartItems.filter((item) => item.id !== action.payload),
      };
    default:
      return state;
  }
};

export default shoppingCartReducer;
