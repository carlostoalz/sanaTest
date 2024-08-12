import { Order } from "./Order.model";
import { Customer } from "./Customer.model";
import { OrderProduct } from "./OrderProduct.model";

export interface OrderDTO {
  Customer: Customer;
  Order: Order;
  OrderProducts: OrderProduct[];
}
