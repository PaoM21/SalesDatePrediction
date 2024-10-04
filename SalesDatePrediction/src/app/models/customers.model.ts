export interface Customers {
  custId: number;
  companyName: string;
  contactName: string;
  contactTitle: string;
  address: string;
  city: string;
  region?: string;
  postalCode?: string;
  country: string;
  phone: string;
  fax?: string;
}
