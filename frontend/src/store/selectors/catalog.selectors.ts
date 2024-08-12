import { RootState } from "../store";

export const getProducts = (state: RootState) => state.catalog.products;
export const getLoading = (state: RootState) => state.catalog.loading;
export const getError = (state: RootState) => state.catalog.error;
