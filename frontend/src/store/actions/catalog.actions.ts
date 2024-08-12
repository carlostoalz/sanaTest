import { createAsyncThunk } from "@reduxjs/toolkit";
import client from "../../graphql/apolloClient";
import { FETCH_PRODUCTS_QUERY } from "../../graphql/queries/products";
import { ProductDTO } from "../../models/ProductDTO.model";

export const fetchProducts = createAsyncThunk(
  "catalog/fetchProducts",
  async () => {
    const { data } = await client.query<{ products: ProductDTO[] }>({
      query: FETCH_PRODUCTS_QUERY,
    });
    return data.products;
  }
);
