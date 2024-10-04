export interface OrderProduct {
  empId: number;
  shipperId: number;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  orderDate: Date;
  requiredDate: Date;
  shippedDate?: Date | null;
  freight: number;
  shipCountry: string;
  productId: number;
  unitPrice: number;
  qty: number;
  discount: number;
}
