import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import android.content.Context
import android.content.SharedPreferences
import android.widget.ArrayAdapter
import android.widget.ListView
import androidx.appcompat.app.AppCompatActivity

class AnimalNamesActivity : AppCompatActivity() {
    private lateinit var recyclerView: RecyclerView
    private lateinit var animalNamesAdapter: AnimalNamesAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_animal_names)

        // Initialize the RecyclerView and its adapter
        recyclerView = findViewById(R.id.recyclerView)
        recyclerView.layoutManager = LinearLayoutManager(this)
        animalNamesAdapter = AnimalNamesAdapter(getAnimalNames())
        recyclerView.adapter = animalNamesAdapter

    private fun getAnimalNames(): List<String> {

    return listOf("Lion", "Tiger", "Elephant", "Giraffe", "Zebra", "Kangaroo")
    }
    }
}
class AnimalDetailsActivity : AppCompatActivity() {
    private lateinit var animalName: String
    private lateinit var blockButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
    super.onCreate(savedInstanceState)
    setContentView(R.layout.activity_animal_details)
    animalName = intent.getStringExtra("animalName") ?: ""
    blockButton = findViewById(R.id.blockButton)
        blockButton.setOnClickListener {
            blockAnimal()
        }
    }
    private fun blockAnimal() {
        val sharedPreferences = getSharedPreferences("BlockedAnimals", Context.MODE_PRIVATE)
        val editor = sharedPreferences.edit()
        editor.putBoolean(animalName, true)
        editor.apply()
        setResult(RESULT_OK)
        finish()
    }
}
class ManageBlockActivity : AppCompatActivity() {
    private lateinit var blockedAnimalsListView: ListView
    private lateinit var blockedAnimalsList: MutableList<String>
    private lateinit var blockedAnimalsAdapter: ArrayAdapter<String>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_manage_block)

       
        blockedAnimalsListView = findViewById(R.id.blockedAnimalsListView)
        blockedAnimalsList = getBlockedAnimals()
        blockedAnimalsAdapter = ArrayAdapter(this, android.R.layout.simple_list_item_1, blockedAnimalsList)
        blockedAnimalsListView.adapter = blockedAnimalsAdapter

        
        blockedAnimalsListView.setOnItemClickListener { _, _, position, _ ->
            unblockAnimal(blockedAnimalsList[position])
        }
    }

    private fun getBlockedAnimals(): MutableList<String> {
        
        val sharedPreferences = getSharedPreferences("BlockedAnimals", Context.MODE_PRIVATE)
        val blockedAnimals = sharedPreferences.all
        return blockedAnimals.filterValues { it == true }.keys.toMutableList()
    }

    private fun unblockAnimal(animalName: String) {
        
        val sharedPreferences = getSharedPreferences("BlockedAnimals", Context.MODE_PRIVATE)
        val editor = sharedPreferences.edit()
        editor.remove(animalName)
        editor.apply()

     
        blockedAnimalsList.remove(animalName)
        blockedAnimalsAdapter.notifyDataSetChanged()

        
    }
}


private fun blockAnimal(animalName: String) {
    
    val sharedPreferences = getSharedPreferences("BlockedAnimals", Context.MODE_PRIVATE)
    val editor = sharedPreferences.edit()
    editor.putBoolean(animalName, true)
    editor.apply()

    updateAnimalList()
}


private fun unblockAnimal(animalName: String) {

    val sharedPreferences = getSharedPreferences("BlockedAnimals", Context.MODE_PRIVATE)
    val editor = sharedPreferences.edit()
    editor.remove(animalName)
    editor.apply()

    updateAnimalList()
}


private fun updateAnimalList() {
    
    val sharedPreferences = getSharedPreferences("BlockedAnimals", Context.MODE_PRIVATE)
}





