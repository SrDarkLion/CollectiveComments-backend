

export default function Home() {
  return (
    <div className="flex justify-center pt-5">
      <div className="mx-auto max-w-xl w-full px-4 sm:px-6 lg:px-8">
        <p className="text-center text-lg font-medium mb-8">Gerar Código FeedBack</p>
        <div className="mx-auto w-full">
          <form action="#" className="space-y-4 border rounded-md p-4 sm:p-6 lg:p-8">
          
            <div className="flex flex-col gap-2">
              <label htmlFor="email" className="font-semibold">Nome da Empresa</label>

              <div className="relative">
                <input
                  type="text"
                  className="w-full rounded-lg border border-gray-200 p-4 pe-12 text-base shadow-xs"
                  placeholder=""
                />
              </div>
            </div>

            <div className="flex flex-col gap-2">
              <label htmlFor="email" className="font-semibold">Senha</label>

              <div className="relative">
                <input
                  type="password"
                  className="w-full rounded-lg border border-gray-200 p-4 pe-12 text-base shadow-xs"
                  placeholder=""
                />
              </div>
            </div>

            <div className="flex flex-col gap-2">
              <label htmlFor="email" className="font-semibold">Repetir Senha</label>

              <div className="relative">
                <input
                  type="password"
                  className="w-full rounded-lg border border-gray-200 p-4 pe-12 text-base shadow-xs"
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

          <div className="sm:p-6 lg:p-8">
            <div className="flex justify-between gap-3">
                <div className="w-full">
                  <input
                    type="password"
                    className="w-full rounded-lg border border-gray-200 p-2  text-base shadow-xs"
                    placeholder=""
                  />
                </div>

                <button className="block w-20 rounded-lg bg-indigo-600 px-5 p-0 text-sm font-medium text-white">Copiar</button>
            </div>
            <p className="mt-1 text-red-500 font-semibold">Observação: Salvar código em um local seguro  </p>
              
          </div>
        </div>
      </div>

    </div>
  );
}
