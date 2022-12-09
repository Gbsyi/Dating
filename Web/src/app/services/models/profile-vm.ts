import { GenderVm } from './gender-vm';

export interface ProfileVm {
  name: string;
  age: number;
  description: string;
  sex: GenderVm;
}
