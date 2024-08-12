import { Product } from "./Product.model";

export interface ProductDTO extends Product {
  categories: string;
  quantity: number;
}
