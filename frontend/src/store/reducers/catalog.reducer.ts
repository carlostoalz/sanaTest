import { createSlice, SerializedError } from "@reduxjs/toolkit";
import { ProductDTO } from "../../models/ProductDTO.model";
import { fetchProducts } from "../actions/catalog.actions";

interface CatalogState {
  products: ProductDTO[];
  loading: boolean;
  error: SerializedError;
}

const initialState: CatalogState = {
  products: [],
  loading: false,
  error: null,
};

export const catalogSlice = createSlice({
  name: "Catalog",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProducts.pending, (state) => ({
        ...state,
        products: [],
        loading: true,
      }))
      .addCase(fetchProducts.fulfilled, (state, { payload }) => ({
        ...state,
        products: payload,
        loading: false,
      }))
      .addCase(fetchProducts.rejected, (state, { error }) => ({
        ...state,
        error,
        loading: false,
      }));
  },
});

export default catalogSlice.reducer;
