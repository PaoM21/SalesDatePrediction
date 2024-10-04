export interface Employees {
  empId: number;
  lastName: string;
  firstName: string;
  title: string;
  titleOfCourtesy: string;
  birthDate: Date;
  hireDate: Date;
  address: string;
  city: string;
  region?: string;
  postalCode?: string;
  country: string;
  phone: string;
  mgrid?: number;
}
