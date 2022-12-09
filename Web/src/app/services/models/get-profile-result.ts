import { ProfileVm } from './profile-vm';

export interface GetProfileResult {
  isCreated: boolean;
  profile?: ProfileVm;
}
