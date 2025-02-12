
export default function CreateCodeForm(){
    return(
        <form action="#" className="space-y-4 border rounded-md p-4 sm:p-6 lg:p-8">
          
        <div className="flex flex-col gap-2">
          <label htmlFor="name" className="font-semibold">Nome da Empresa</label>

          <div className="relative">
            <input
              type="text"
              className="w-full rounded-lg border border-gray-200 p-2 pe-12 text-base shadow-xs"
              name="name"
              placeholder=""
            />
          </div>
        </div>

        <div className="flex flex-col gap-2">
          <label htmlFor="password" className="font-semibold">Senha</label>

          <div className="relative">
            <input
              type="password"
              className="w-full rounded-lg border border-gray-200 p-2 pe-12 text-base shadow-xs"
              name="password"
              placeholder=""
            />
          </div>
        </div>

        <div className="flex flex-col gap-2">
          <label htmlFor="repeatpassoword" className="font-semibold">Repetir Senha</label>

          <div className="relative">
            <input
              type="password"
              className="w-full rounded-lg border border-gray-200 p-2  pe-12 text-base shadow-xs"
              name="repeatpassoword"
              placeholder=""
            />
          </div>
        </div>

        <button
          type="submit"
          className="block w-full rounded-lg bg-gray-950 hover:bg-gray-900 px-5 py-3 text-sm font-medium text-white"
        >
          Gerar Código
        </button>
      </form>

    )
}