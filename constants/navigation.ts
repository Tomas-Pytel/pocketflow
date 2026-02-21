import { FaPiggyBank } from "react-icons/fa6";
import { IoHome } from "react-icons/io5";
import { IconType } from "react-icons/lib";
import { MdWallet } from "react-icons/md";
import { RiBarChart2Fill } from "react-icons/ri";

export interface NavLinkProps {
  href: string;
  label: string;
  icon: IconType;
  className?: string;
}

export const NAV_LINKS: NavLinkProps[] = [
  {
    href: "/dashboard",
    label: "Home",
    icon: IoHome,
  },
  {
    href: "/budgets",
    label: "Budgets",
    icon: MdWallet,
  },
  {
    href: "/savings",
    label: "Savings",
    icon: FaPiggyBank,
  },
  {
    href: "/analytics",
    label: "Analytics",
    icon: RiBarChart2Fill,
  },
];
