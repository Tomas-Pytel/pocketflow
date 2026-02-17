import Image from "next/image";

export default function UserCard({
  name,
  image,
}: Readonly<{
  image?: string;
  name: string;
}>) {
  return (
    <div className="flex items-center space-x-2">
      <Image
        src={image || "/user-pic.png"}
        alt={name}
        width={30}
        height={30}
        className="rounded-full border-2 border-orange-500"
      />
      <div className="flex flex-col">
        <span className="text-xs text-gray-500">Welcome back,</span>
        <span className="text-xs font-semibold text-gray-700">{name}</span>
      </div>
    </div>
  );
}
