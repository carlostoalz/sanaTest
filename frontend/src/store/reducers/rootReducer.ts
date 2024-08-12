import { combineReducers } from "@reduxjs/toolkit";
import catalogReducer from "./catalog.reducer";
import shoppingCartReducer from "./shoppingCart.reducer";

const rootReducer = combineReducers({
  catalog: catalogReducer,
  shoppingCart: shoppingCartReducer,
});

export default rootReducer;
