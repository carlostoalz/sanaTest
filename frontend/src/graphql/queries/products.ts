import { gql } from "@apollo/client";

export const FETCH_PRODUCTS_QUERY = gql`
  query {
    products {
      id
      name
      code
      description
      price
      stock
      img
      categories
    }
  }
`;
