import Image from "next/image";


export default function Header(){

    return(
        <header className="flex justify-center p-3 border-b">
            <Image
                src={"/assets/logo.svg"}
                alt="Logos"
                width={235}
                height={101}
                priority
            />
        </header>
    )
}